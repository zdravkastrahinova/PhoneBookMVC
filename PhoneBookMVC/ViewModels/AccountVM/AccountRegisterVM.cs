using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.AccountVM
{
    public class AccountRegisterVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please input username. It is required!")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Username should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z -]+)$", ErrorMessage = "Username can consist only letters, spaces and dashes.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please input email. It is required!")]
        [EmailAddress(ErrorMessage = "Email address is not correct.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please input first name. It is required!")]
        [Display(Name = "First Name")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "First name should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z]+)$", ErrorMessage = "First name can consist only letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please input last name. It is required!")]
        [Display(Name = "Last Name")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Last name should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z]+)$", ErrorMessage = "Last name can consist only letters.")]
        public string LastName { get; set; }

        public string RedirectUrl { get; set; }
    }
}