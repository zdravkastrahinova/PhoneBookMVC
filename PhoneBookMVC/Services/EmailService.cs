using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.Services
{
    public static class EmailService
    {
        public static void SendEmail(User user, ControllerContext context)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("confirmAccount@gmail.com");
            mail.To.Add(user.Email);
            mail.Subject = "Account confirmation";
           
            string parameters = "?userID=" + user.ID + "&key=" + user.Password;
            var path = context.HttpContext.Request.Url.Host;
            var port = context.HttpContext.Request.Url.Port;

            mail.Body = "http://" + path + ":" + port + "/Account/Confirm" + parameters;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("phonebook.pro@gmail.com", "phonebook");
            smtp.Send(mail);
        }     
    }
}