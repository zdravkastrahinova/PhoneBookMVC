using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class PhonesRepository : BaseRepository<Phone>
    {
        public PhonesRepository() : base() { }

        public PhonesRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}