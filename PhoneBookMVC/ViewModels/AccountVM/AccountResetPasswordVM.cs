using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.AccountVM
{
    public class AccountResetPasswordVM
    {
        [Required(ErrorMessage = "Please input email. It is required!")]
        [EmailAddress(ErrorMessage = "Email address is not correct.")]
        public string Email { get; set; }
    }
}