﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<Contact> Contacts { get; set; }
        public virtual List<Group> Groups { get; set; }
    }
}