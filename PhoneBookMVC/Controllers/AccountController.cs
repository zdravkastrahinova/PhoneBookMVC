using AutoMapper;
using PhoneBookMVC.Filters;
using PhoneBookMVC.Models;
using PhoneBookMVC.Services;
using PhoneBookMVC.Services.ModelServices;
using PhoneBookMVC.ViewModels.AccountVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

namespace PhoneBookMVC.Controllers
{
    public class AccountController : Controller
    {
        [AuthorizeAccessFilter]
        public ActionResult Login(string redirectUrl)
        {
            AccountLoginVM model = new AccountLoginVM();
            model.RedirectUrl = redirectUrl;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccessFilter]
        public ActionResult Login()
        {
            AccountLoginVM model = new AccountLoginVM();
            TryUpdateModel(model);

            AuthenticationManager.AuthenticateUser(model.Username, model.Password);

            if (AuthenticationManager.LoggedUser == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid username or password.");
                return View(model);
            }

            if (model.IsRemembered)
            {
                CookieService.CreateCookie();
            }

            if (!String.IsNullOrEmpty(model.RedirectUrl))
            {
                return Redirect(model.RedirectUrl);
            }

            return this.RedirectToAction<ContactsController>(c => c.List());
        }

        public ActionResult Logout()
        {
            AuthenticationManager.Logout();
            return this.RedirectToAction(c => c.Login());
        }

        [AuthorizeAccessFilter]
        public ActionResult Register(string redirectUrl)
        {
            AccountRegisterVM model = new AccountRegisterVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccessFilter]
        public ActionResult Register()
        {
            UsersService usersService = new UsersService();
            AccountRegisterVM model = new AccountRegisterVM();
            TryUpdateModel(model);

            if (usersService.IsUserExsits(model))
            {
                ModelState.AddModelError(String.Empty, "Username or Email is already taken.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user;
            if (model.ID == 0)
            {
                user = new User();
            }
            else
            {
                return this.RedirectToAction(c => c.Login());
            }

            Mapper.Map(model, user);
            user.Password = Guid.NewGuid().ToString();

            usersService.Save(user);
            Task.Run(() => EmailService.SendEmail(user, ControllerContext));

            return View("WaitForConfirmation");
        }

        [AuthorizeAccessFilter]
        public ActionResult Confirm(int userID, string key)
        {
            AccountConfirmVM model = new AccountConfirmVM();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeAccessFilter]
        public ActionResult Confirm()
        {
            UsersService usersService = new UsersService();
            AccountConfirmVM model = new AccountConfirmVM();
            TryUpdateModel(model);

            User user;
            user = usersService.GetByID(model.UserID);

            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "User not exist.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            user.Password = model.Password;
            usersService.Save(user);

            return this.RedirectToAction(c => c.Login());
        }

        public ActionResult ResetPassword()
        {
            AccountResetPasswordVM model = new AccountResetPasswordVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult ResetPassword(AccountResetPasswordVM model)
        {
            UsersService usersService = new UsersService();
            TryUpdateModel(model);

            User user = usersService.GetAll().FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "Invalid email address.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Task.Run(() => EmailService.SendEmail(user, ControllerContext));

            return View("WaitForConfirmation");
        }
    }
}