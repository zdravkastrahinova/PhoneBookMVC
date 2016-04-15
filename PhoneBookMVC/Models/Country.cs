﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.Models
{
    public class Country : BaseModel
    {
        public string Name { get; set; }

        public virtual List<City> Cities { get; set; }
    }
}