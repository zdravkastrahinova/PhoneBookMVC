using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using PhoneBookMVC.Services.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services
{
    public static class CookieService
    {
        public static void CreateCookie()
        {
            UsersService usersService = new UsersService();
            User user = usersService.GetByID(AuthenticationService.LoggedUser.ID);

            if (user != null)
            {
                HttpCookie cookie = new HttpCookie("rememberMe");    
                cookie.Name = "rememberMe";
                cookie.Value = Guid.NewGuid().ToString();
                cookie.Expires = DateTime.Now.AddMinutes(10);
                HttpContext.Current.Response.Cookies.Add(cookie);

                user.RememberMeHash = cookie.Value;
                user.RememberMeExpiryDate = DateTime.Now.AddMinutes(10);
                usersService.Save(user);
            }
        }

        public static void DeleteCookie()
        {
            HttpCookie cookie = new HttpCookie("rememberMe");
            cookie.Expires = DateTime.Now.AddMinutes(-10);
            HttpContext.Current.Response.Cookies.Set(cookie);

            UsersService usersService = new UsersService();
            User user = usersService.GetByID(AuthenticationService.LoggedUser.ID);

            user.RememberMeHash = null;
            user.RememberMeExpiryDate = null;

            usersService.Save(user);
        }
    }
}