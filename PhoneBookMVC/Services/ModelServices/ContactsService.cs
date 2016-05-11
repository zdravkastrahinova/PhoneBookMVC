using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Services.ModelServices
{
    public class ContactsService : BaseService<Contact>
    {
        public ContactsService() : base() { }

        public ContactsService(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public User GetUser(int userID)
        {
            return new UsersRepository().GetByID(userID);
        }

        public IEnumerable<SelectListItem> GetSelectedCountries()
        {
            return new CountriesRepository().GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetSelectedCities()
        {
            return new CitiesRepository().GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetCitiesByCountryID(int countryID)
        {
            return new CitiesRepository().GetAll().Where(c => c.CountryID == countryID).ToList().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.ID.ToString()
            });
        }

        public IEnumerable<SelectListItem> GetSelectedGroups(List<Group> groups, string[] selectedGroups = null)
        {
            if (groups == null)
            {
                groups = new List<Group>();
            }

            List<string> selectedIds = groups.Select(g => g.ID.ToString()).ToList();

            if (selectedGroups != null)
            {
                selectedIds.AddRange(selectedGroups);
            }

            return new GroupsRepository().GetAll().Where(g => g.UserID == AuthenticationManager.LoggedUser.ID).Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.ID.ToString(),
                Selected = selectedIds.Contains(g.ID.ToString())
            });
        }

        public void UpdateContactGroups(Contact contact, string[] selectedIds)
        {
            if (selectedIds == null)
            {
                selectedIds = new string[0];
            }

            if (contact.Groups == null)
            {
                contact.Groups = new List<Group>();
            }

            contact.Groups.Clear();
            foreach (Group group in new GroupsRepository(base.unitOfWork).GetAll())
            {
                if (selectedIds.Contains(group.ID.ToString()))
                {
                    contact.Groups.Add(group);
                }
            }
        }
    }
}