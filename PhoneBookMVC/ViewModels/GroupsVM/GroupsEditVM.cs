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

        [Required(ErrorMessage = "Please input group name. It is required!")]
        [Display(Name = "Name")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Group name should contain between 3 and 70 characters.")]
        [RegularExpression(@"^([A-z]+)$", ErrorMessage = "Group name can consist only letters.")]
        public string Name { get; set; }

        public int UserID { get; set; }
    }
}