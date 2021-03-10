using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace IssueTrackerProject.Models
{
    public class UserLogin
    {
        [Display(Name = "User Id")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Id is required")]
        public string UserId { get; set; }
        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}