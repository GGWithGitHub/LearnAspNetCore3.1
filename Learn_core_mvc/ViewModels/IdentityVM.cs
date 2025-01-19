using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class VerifyTwoFactorTokenVM
    {
        public string? Email { get; set; }
        public bool RememberMe { get; set; }
        public string? Token { get; set; }

        [Required]
        [Display(Name = "Two Factor Code")]
        public string? TwoFactorCode { get; set; }
    }
}
