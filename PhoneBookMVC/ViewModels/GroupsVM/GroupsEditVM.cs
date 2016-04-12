using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBookMVC.ViewModels.GroupsVM
{
    public class GroupsEditVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please input first name. It is required!")]
        [Display(Name = "Name")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Name should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z]+)$", ErrorMessage = "Name can consist only letters.")]
        public string Name { get; set; }

        public int UserID { get; set; }
    }
}