using PhoneBookMVC.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.ViewModels.PhonesVM
{
    public class PhonesEditVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please input phone number. It is required!")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Phone number can consist only digits.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please select phone type. It is required!")]
        [Display(Name = "Phone Type")]
        public PhoneTypeEnum PhoneType { get; set; }

        public int ContactID { get; set; }
    }
}