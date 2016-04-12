using PagedList;
using PhoneBookMVC.Enums;
using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.ViewModels.ContactsVM
{
    public class ContactsListVM : ListVM
    {
        public IPagedList<Contact> Contacts { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }
    }
}