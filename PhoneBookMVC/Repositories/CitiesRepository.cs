using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Repositories
{
    public class CitiesRepository : BaseRepository<City>
    {
        public CitiesRepository() : base() { }
        public CitiesRepository(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}