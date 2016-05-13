using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.ViewModels.GroupsVM
{
    public class GroupsListVM : ListVM
    {
        public Dictionary<Group, List<SelectListItem>> Groups  { get; set; }
    }
}