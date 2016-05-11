using AutoMapper;
using PagedList;
using PagedList.Mvc;
using PhoneBookMVC.Filters;
using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using PhoneBookMVC.Services;
using PhoneBookMVC.Services.ModelServices;
using PhoneBookMVC.ViewModels.ContactsVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace PhoneBookMVC.Controllers
{
    public class ContactsController : BaseController
    {
        public ActionResult List()
        {
            ContactsService contactsService = new ContactsService();
            ContactsListVM model = new ContactsListVM();
            TryUpdateModel(model);

            List<Contact> contacts = contactsService.GetAll().Where(c => c.UserID == AuthenticationManager.LoggedUser.ID).ToList();

            if (!String.IsNullOrEmpty(model.Search))
            {            
                model.Search = model.Search.Replace(" ", String.Empty);
                contacts = contacts.Where(c => (c.FirstName + c.LastName).ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch (model.SortOrder)
            {
                case "lname_asc": contacts = contacts.OrderBy(c => c.LastName).ToList(); break;
                case "lname_desc": contacts = contacts.OrderByDescending(c => c.LastName).ToList(); break;
                case "fname_desc": contacts = contacts.OrderByDescending(c => c.FirstName).ToList(); break;
                case "fname_asc":
                default: contacts = contacts.OrderBy(c => c.FirstName).ToList(); break;
            }

            int pageSize = 3;
            if (model.PageSize != 0)
            {
                pageSize = model.PageSize;
            }

            int pageNumber = model.Page ?? 1;
            model.Contacts = contacts.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            ContactsService contactsService = new ContactsService();
            ContactsEditVM model = new ContactsEditVM();

            Contact contact;
            if (!id.HasValue)
            {
                contact = new Contact();
                model.CountryID = int.Parse(contactsService.GetSelectedCountries().FirstOrDefault().Value);
            }
            else
            {
                contact = contactsService.GetByID(id.Value);
                if (contact == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
                model.CountryID = contact.City.CountryID;
            }

            Mapper.Map(contact, model);

            model.Countries = contactsService.GetSelectedCountries();
            model.Cities = contactsService.GetCitiesByCountryID(model.CountryID);
            model.Groups = contactsService.GetSelectedGroups(contact.Groups);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            ContactsService contactsService = new ContactsService(unitOfWork);

            ContactsEditVM model = new ContactsEditVM();
            TryUpdateModel(model);

            Contact contact;
            if (model.ID == 0)
            {
                contact = new Contact();
            }
            else
            {
                contact = contactsService.GetByID(model.ID);
                if (contact == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
            {
                var imageExtension = Path.GetExtension(model.ImageUpload.FileName);

                if (String.IsNullOrEmpty(imageExtension) || !imageExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError(string.Empty, "Wrong image format.");
                }
                else
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    model.ImagePath = model.ImageUpload.FileName;
                    model.ImageUpload.SaveAs(filePath + model.ImagePath);
                }
            }

            if (!ModelState.IsValid)
            {
                model.Countries = contactsService.GetSelectedCountries();
                model.Cities = contactsService.GetCitiesByCountryID(model.CountryID);
                model.Groups = contactsService.GetSelectedGroups(contact.Groups, model.SelectedGroups);

                return View(model);
            }

            Mapper.Map(model, contact);
            contact.UserID = AuthenticationManager.LoggedUser.ID;

            contactsService.UpdateContactGroups(contact, model.SelectedGroups);
            contactsService.Save(contact);

            return this.RedirectToAction(c => c.List());
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.RedirectToAction(c => c.List());
            }

            UnitOfWork unitOfWork = new UnitOfWork();
            ContactsService contactsService = new ContactsService(unitOfWork);

            contactsService.GetByID(id.Value).Groups.Clear();
            contactsService.Delete(id.Value);

            return this.RedirectToAction(c => c.List());
        }

        public JsonResult DeleteImage(int contactID)
        {
            ContactsService contactsService = new ContactsService();
            Contact contact = contactsService.GetByID(contactID);

            contact.ImagePath = "default.jpg";
            contactsService.Save(contact);

            return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowCities(int countryID)
        {
            ContactsService contactsService = new ContactsService();
            List<SelectListItem> cities = contactsService.GetCitiesByCountryID(countryID).ToList();

            return Json(cities.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}