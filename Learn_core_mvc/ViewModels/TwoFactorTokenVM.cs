using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class TwoFactorTokenVM
    {
        [Required(ErrorMessage = "Security code is required.")]
        [Display(Name = "Security Code")]
        public string TwoFactorToken { get; set; }
    }
}
