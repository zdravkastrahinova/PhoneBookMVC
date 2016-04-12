using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class ContactsRepository : BaseRepository<Contact>
    {
        public ContactsRepository() : base() { }

        public ContactsRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}