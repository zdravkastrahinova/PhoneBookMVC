using PagedList;
using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.UsersVM
{
    public class UsersListVM : ListVM
    {
        public IPagedList<User> Users { get; set; }
    }
}