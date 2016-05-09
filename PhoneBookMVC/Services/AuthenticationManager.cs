using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services
{
    public static class AuthenticationManager
    {
        public static AuthenticationService AuthInstance
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session[typeof(AuthenticationService).Name] == null)
                {
                    HttpContext.Current.Session[typeof(AuthenticationService).Name] = new AuthenticationService();
                }

                return (AuthenticationService)HttpContext.Current.Session[typeof(AuthenticationService).Name];
            }
        }

        public static User LoggedUser
        {
            get
            {
                return AuthInstance.CurrentUser;
            }
        }

        public static void AuthenticateUser(string username, string password)
        {
             AuthInstance.AuthenticateUser(username, password);
        }

        public static void AuthenticateUserByCookie(HttpCookie cookie)
        {
            AuthenticateUserByCookie(cookie);
        }

        public static void Logout()
        {
            CookieService.DeleteCookie();
            AuthenticateUser(null, null);
        }
    }
}