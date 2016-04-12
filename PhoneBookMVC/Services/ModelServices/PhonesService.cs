using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services.ModelServices
{
    public class PhonesService : BaseService<Phone>
    {
        public PhonesService() : base() { }

        public PhonesService(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public Contact GetContact(int contactID)
        {
            return new ContactsRepository().GetByID(contactID);
        }

        public int GetContactID(int phoneID)
        {
            return GetByID(phoneID).ContactID;
        }
    }
}