using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.AccountVM
{
    public class AccountConfirmVM
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please input password. It is required!")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Password should contain between 3 and 70 characters.")]
        public string Password { get; set; }

        public string Key { get; set; }
    }
}