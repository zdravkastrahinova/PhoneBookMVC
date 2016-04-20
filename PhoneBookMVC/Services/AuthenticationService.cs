﻿using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services
{
    public static class AuthenticationService
    {
        public static User LoggedUser { get; set; }

        public static void AuthenticateUser(string username, string password)
        {
            UsersRepository usersRepo = new UsersRepository();
            LoggedUser = usersRepo.GetAll().FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public static void AuthenticateUserByCookie(HttpCookie cookie)
        {
            UsersRepository usersRepo = new UsersRepository();
            LoggedUser = usersRepo.GetAll().FirstOrDefault(u => u.RememberMeHash == cookie.Value);
        }

        public static void Logout()
        {
            CookieService.DeleteCookie();
            AuthenticateUser(null, null);
        }
    }
}