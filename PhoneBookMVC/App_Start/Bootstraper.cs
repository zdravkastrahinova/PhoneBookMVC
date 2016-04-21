using PhoneBookMVC.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.App_Start
{
    public static class Bootstraper
    {
        public static void Run()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}