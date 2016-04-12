using PagedList;
using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.GroupsVM
{
    public class GroupsListVM : ListVM
    {
        public IPagedList<Group> Groups { get; set; }
    }
}