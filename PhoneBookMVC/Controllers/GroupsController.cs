using PagedList;
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

            List<Group> groups = groupsService.GetAll();

            if (!String.IsNullOrEmpty(model.Search))
            {
                groups = groups.Where(g => g.Name.ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch (model.SortOrder)
            {
                case "name_desc": groups = groups.OrderByDescending(g => g.Name).ToList(); break;
                case "name_asc":
                default: groups = groups.OrderBy(g => g.Name).ToList(); break;
            }

            int pageSize = 2;
            if (model.PageSize != 0)
            {
                pageSize = model.PageSize;
            }

            int pageNumber = model.Page ?? 1;
            model.Groups = groups.ToPagedList(pageNumber, pageSize);

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

            model.ID = group.ID;
            model.Name = group.Name;
            model.UserID = group.UserID;

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

            group.ID = model.ID;
            group.Name = model.Name;
            group.UserID = AuthenticationService.LoggedUser.ID;
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
    }
}