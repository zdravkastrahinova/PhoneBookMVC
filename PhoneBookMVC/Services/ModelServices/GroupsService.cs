using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Services.ModelServices
{
    public class GroupsService : BaseService<Group>
    {
        public GroupsService() : base() { }

        public GroupsService(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public IEnumerable<SelectListItem> GetSelectedContacts(Group group)
        {
            List<string> selectedIds = group.Contacts.Select(c => c.ID.ToString()).ToList();

            return new ContactsRepository().GetAll().Select(c => new SelectListItem
            {
                Text = c.FirstName + " " + c.LastName,
                Value = c.ID.ToString(),
                Selected = selectedIds.Contains(c.ID.ToString())
            });
        }
    }
}