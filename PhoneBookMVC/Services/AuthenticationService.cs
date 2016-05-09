using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services
{
    public class AuthenticationService
    {
        public User CurrentUser { get; set; }

        public void AuthenticateUser(string username, string password)
        {
            UsersRepository usersRepo = new UsersRepository();
            CurrentUser = usersRepo.GetAll().FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public void AuthenticateByCookie(HttpCookie cookie)
        {
            UsersRepository usersRepo = new UsersRepository();
            CurrentUser = usersRepo.GetAll().FirstOrDefault(u => u.RememberMeHash == cookie.Value);
        }
    }
}