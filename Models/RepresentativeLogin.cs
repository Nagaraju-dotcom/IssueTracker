using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IssueTrackerProject.Models
{
    public class RepresentativeLogin
    {
        [Display(Name = "Representative Id ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Representative Id is required")]
        public string UserId { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}