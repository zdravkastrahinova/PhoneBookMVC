﻿using PhoneBookMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Filters
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["rememberMe"];

            if (cookie != null && AuthenticationManager.LoggedUser == null)
            {
                AuthenticationManager.AuthenticateUserByCookie(cookie);
            }

            if (AuthenticationManager.LoggedUser == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Account/Login?RedirectUrl=" + filterContext.HttpContext.Request.Url);
                filterContext.Result = new EmptyResult();
            }
        }
    }
}