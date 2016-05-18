using PhoneBookMVC.Models;
using PhoneBookMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Services.ModelServices
{
    public class CountriesService : BaseService<Country>
    {
        public CountriesService() : base() { }

        public CountriesService(UnitOfWork unitOfWork) : base(unitOfWork) { }        
    }
}