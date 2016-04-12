using PhoneBookMVC.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Models
{
    public class Phone : BaseModel
    {
        public string PhoneNumber { get; set; }
        public PhoneTypeEnum PhoneType { get; set; }

        public int ContactID { get; set; }
        public virtual Contact Contact { get; set; }
    }
}