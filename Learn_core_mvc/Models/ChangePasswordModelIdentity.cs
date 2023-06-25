using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class ChangePasswordModelIdentity
    {
        [Required]
        [Display(Name ="Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Compare("NewPassword",ErrorMessage = "New and confirm password is not same")]
        [Display(Name = "Confirm password")]
        public string ConfirmNewPassword { get; set; }
    }
}
