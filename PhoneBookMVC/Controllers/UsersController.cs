﻿using PagedList;
using PagedList.Mvc;
using PhoneBookMVC.Filters;
using PhoneBookMVC.Models;
using PhoneBookMVC.Services.ModelServices;
using PhoneBookMVC.ViewModels.UsersVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    public class UsersController : BaseController
    {
        public ActionResult List()
        {
            UsersService usersService = new UsersService();
            UsersListVM model = new UsersListVM();
            TryUpdateModel(model);

            List<User> users = usersService.GetAll();

            if (!String.IsNullOrEmpty(model.Search))
            {
                users = users.Where(u => u.Username.ToLower().Contains(model.Search.ToLower()) || u.FirstName.ToLower().Contains(model.Search.ToLower()) || u.LastName.ToLower().Contains(model.Search.ToLower())).ToList();
            }

            switch (model.SortOrder)
            {
                case "lname_asc": users = users.OrderBy(u => u.LastName).ToList(); break;
                case "lname_desc": users = users.OrderByDescending(u => u.LastName).ToList(); break;
                case "fname_desc": users = users.OrderByDescending(u => u.FirstName).ToList(); break;
                case "fname_asc": users = users.OrderBy(u => u.FirstName).ToList(); break;
                case "username_desc": users = users.OrderByDescending(u => u.Username).ToList(); break;
                case "username_asc":
                default: users = users.OrderBy(u => u.Username).ToList(); break;
            }

            int pageSize = 2;
            if (model.PageSize != 0)
            {
                pageSize = model.PageSize;
            }

            int pageNumber = model.Page ?? 1;
            model.Users = users.ToPagedList(pageNumber, pageSize);

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            UsersService usersService = new UsersService();
            UsersEditVM model = new UsersEditVM();
            User user;

            if (!id.HasValue)
            {
                user = new User();
            }
            else
            {
                user = usersService.GetByID(id.Value);
                if (user == null)
                {
                    return RedirectToAction("List");
                }
            }

            model.ID = user.ID;
            model.Username = user.Username;
            model.Password = user.Password;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            UsersService usersService = new UsersService();
            UsersEditVM model = new UsersEditVM();
            TryUpdateModel(model);
            User user;

            if (model.ID == 0)
            {
                user = new User();
            }
            else
            {
                user = usersService.GetByID(model.ID);
                if (user == null)
                {
                    return RedirectToAction("List");
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.ID = model.ID;
            user.Username = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            usersService.Save(user);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            UsersService usersService = new UsersService();
            usersService.Delete(id.Value);

            return RedirectToAction("List");
        }
    }
}