using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.PhonesVM
{
    public class PhonesListVM
    {
        public List<Phone> Phones { get; set; }
        public int? ContactID { get; set; }
        public Contact Contact { get; set; }
    }
}