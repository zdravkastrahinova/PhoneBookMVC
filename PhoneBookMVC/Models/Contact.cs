using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Models
{
    public class Contact : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        public virtual List<Phone> Phones { get; set; }
        public virtual List<Group> Groups { get; set; }
    }
}