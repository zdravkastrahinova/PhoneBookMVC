using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Models
{
    public class City : BaseModel
    {
        public string Name { get; set; }

        public int CountryID { get; set; }
        public virtual Country Country { get; set; }

        public virtual List<Contact> Contacts { get; set; }
    }
}