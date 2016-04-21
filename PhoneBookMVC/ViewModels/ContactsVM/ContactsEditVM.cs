using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBookMVC.ViewModels.ContactsVM
{
    public class ContactsEditVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please input first name. It is required!")]
        [Display(Name = "First Name")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "First name should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z -])+$", ErrorMessage = "First name can consist letters, spaces and dashes.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please input last name. It is required!")]
        [Display(Name = "Last Name")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Last name should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z -])+$", ErrorMessage = "Last name can consist letters, spaces and dashes.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please input address. It is required!")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Address should contain between 3 and 70 characters.")]
        [RegularExpression(@"^[^<>()!@#%/^?&_|)*]+$", ErrorMessage = "Address can consisist only letters and digits.")]
        public string Address { get; set; }

        public int UserID { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }
        public string[] SelectedGroups { get; set; }

        public string ImagePath { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }

        [Display(Name = "Country")]
        public int CountryID { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        [Display(Name = "City")]
        public int CityID { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}