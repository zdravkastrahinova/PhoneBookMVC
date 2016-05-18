using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services.ModelServices
{
    public class CitiesService : BaseService<City>
    {
        public CitiesService() : base() { }

        public CitiesService(UnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}