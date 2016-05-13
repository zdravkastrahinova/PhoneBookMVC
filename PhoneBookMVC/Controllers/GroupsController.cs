using AutoMapper;
using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using PhoneBookMVC.Services;
using PhoneBookMVC.Services.ModelServices;
using PhoneBookMVC.ViewModels.GroupsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace PhoneBookMVC.Controllers
{
    public class GroupsController : BaseController
    {
        public ActionResult List()
        {
            GroupsService groupsService = new GroupsService();
            GroupsListVM model = new GroupsListVM();
            TryUpdateModel(model);

            model.Groups = new Dictionary<Group, List<SelectListItem>>();

            foreach (var group in groupsService.GetAll().Where(g => g.UserID == AuthenticationManager.LoggedUser.ID))
            {
                List<SelectListItem> contacts = groupsService.GetSelectedContacts(group).ToList();
                model.Groups.Add(group, contacts);
            }

            if (!String.IsNullOrEmpty(model.Search))
            {
                model.Groups = model.Groups.Where(g => g.Key.Name.ToLower().Contains(model.Search.ToLower())).ToDictionary(v => v.Key, v => v.Value);
            }

            switch (model.SortOrder)
            {
                case "name_desc": model.Groups = model.Groups.OrderByDescending(g => g.Key.Name).ToDictionary(v => v.Key, v => v.Value); break;
                case "name_asc":
                default: model.Groups = model.Groups.OrderBy(g => g.Key.Name).ToDictionary(v => v.Key, v => v.Value); break;
            }
            
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            GroupsService groupsService = new GroupsService();
            GroupsEditVM model = new GroupsEditVM();

            Group group;
            if (!id.HasValue)
            {
                group = new Group();
            }
            else
            {
                group = groupsService.GetByID(id.Value);
                if (group == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            Mapper.Map(group, model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            GroupsService groupsService = new GroupsService();
            GroupsEditVM model = new GroupsEditVM();
            TryUpdateModel(model);

            Group group;
            if (model.ID == 0)
            {
                group = new Group();
            }
            else
            {
                group = groupsService.GetByID(model.ID);
                if (group == null)
                {
                    return this.RedirectToAction(c => c.List());
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Mapper.Map(model, group);
            group.UserID = AuthenticationManager.LoggedUser.ID;

            groupsService.Save(group);

            return this.RedirectToAction(c => c.List());
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return this.RedirectToAction(c => c.List());
            }

            UnitOfWork unitOfWork = new UnitOfWork();
            GroupsService groupsService = new GroupsService(unitOfWork);

            groupsService.GetByID(id.Value).Contacts.Clear();
            groupsService.Delete(id.Value);

            return this.RedirectToAction(c => c.List());
        }

        public JsonResult Remove(int contactID, int groupID)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            GroupsService groupsService = new GroupsService(unitOfWork);

            Group group = groupsService.GetByID(groupID);
            group.Contacts = group.Contacts.Where(c => c.ID != contactID).ToList();

            groupsService.Save(group);

            return Json(new object[] { new object() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(int[] contactIDs, int groupID)
        {
            UnitOfWork unitOfWork = new UnitOfWork();
            GroupsService groupsService = new GroupsService(unitOfWork);
            
            if (contactIDs == null)
            {
                contactIDs = new int[0];
            }

            Group group = groupsService.GetByID(groupID);
            group.Contacts.Clear();

            foreach (var id in contactIDs)
            {
                Contact contact = new ContactsService(unitOfWork).GetByID(id);
                group.Contacts.Add(contact);
            }

            groupsService.Save(group);

            var contacts = group.Contacts.Select(c => new
            {
                id = c.ID,
                firstName = c.FirstName,
                lastName = c.LastName
            });

           return Json(contacts, JsonRequestBehavior.AllowGet);
        }
    }
}