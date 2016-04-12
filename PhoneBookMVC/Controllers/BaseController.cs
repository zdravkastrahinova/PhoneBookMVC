using PhoneBookMVC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Controllers
{
    [AuthenticationFilter]
    public class BaseController : Controller
    {
        
    }
}