﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels
{
    public class ListVM
    {
        public string Search { get; set; }
        public string SortOrder { get; set; }

        public int? Page { get; set; }
        public int PageSize { get; set; }
    }
}