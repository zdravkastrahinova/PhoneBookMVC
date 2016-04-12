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
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    public class ContactsController : BaseController
    {
        public ActionResult List()
        {
            ContactsService contactsService = new ContactsService();
            ContactsListVM model = new ContactsListVM();
            TryUpdateModel(model);

            List<Contact> contacts = contactsService.GetAll().Where(c => c.UserID == AuthenticationService.LoggedUser.ID).ToList();

            if (!String.IsNullOrEmpty(model.Search))
            {
                contacts = contacts.Where(c => c.FirstName.ToLower().Contains(model.Search.ToLower()) || c.LastName.ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch(model.SortOrder)
            {
                case "lname_asc": contacts = contacts.OrderBy(c => c.LastName).ToList(); break;
                case "lname_desc": contacts = contacts.OrderByDescending(c => c.LastName).ToList(); break;
                case "fname_desc": contacts = contacts.OrderByDescending(c => c.FirstName).ToList(); break;
                case "fname_asc":
                default: contacts = contacts.OrderBy(c => c.FirstName).ToList(); break;
            }

            int pageSize = 2;
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
            }
            else
            {
                contact = contactsService.GetByID(id.Value);
                if (contact == null)
                {
                    return RedirectToAction("List");
                }
            }

            model.ID = contact.ID;
            model.FirstName = contact.FirstName;
            model.LastName = contact.LastName;
            model.Address = contact.Address;
            model.UserID = contact.UserID;
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
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Groups = contactsService.GetSelectedGroups(contact.Groups, model.SelectedGroups);
                return View(model);
            }

            contact.ID = model.ID;
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.Address = model.Address;
            contact.UserID = AuthenticationService.LoggedUser.ID;

            contactsService.UpdateContactGroups(contact, model.SelectedGroups);
            contactsService.Save(contact);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            UnitOfWork unitOfWork = new UnitOfWork();
            ContactsService contactsService = new ContactsService(unitOfWork);

            contactsService.GetByID(id.Value).Groups.Clear();
            contactsService.Delete(id.Value);

            return RedirectToAction("List");
        }
    }
}