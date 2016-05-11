using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.AccountVM
{
    public class AccountLoginVM
    {
        [Required(ErrorMessage = "Please input username. It is required!")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Username should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z-_.])+$", ErrorMessage = "Username can consist only letters, dashes, underscores and fullstops. Spaces are not allowed!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please input password. It is required!")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Password should contain between 3 and 70 characters.")]
        public string Password { get; set; }

        public string RedirectUrl { get; set; }

        [Display(Name = "Remember Me")]
        public bool IsRemembered { get; set; }
    }
}