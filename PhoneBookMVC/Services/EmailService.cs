using PhoneBookMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PhoneBookMVC.Services
{
    public static class EmailService
    {
        public static void SendEmail(User user)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("confirmAccount@gmail.com");
            mail.To.Add(user.Email);
            mail.Subject = "Account confirmation";
           
            string parameters = "?userID=" + user.ID + "&key=" + user.Password;
            var path = HttpContext.Current.Request.Url.Host; // get host
            var port = HttpContext.Current.Request.Url.Port; // get current port

            mail.Body = "http://" + path + ":" + port + "/Account/Confirm" + parameters;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("testforhallmanager@gmail.com", "hallmanager");
            smtp.Send(mail);
        }     
    }
}