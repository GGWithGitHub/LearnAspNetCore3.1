using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class SubmitFormPartialVM
    {
        public SubmitFormPVVM SubmitFormPVVM { get; set; }
    }
    public class SubmitFormPVVM
    {
        [Required(ErrorMessage ="Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(8,ErrorMessage ="Password must contain 5 to 8 characters.",MinimumLength =5)]
        public string Password { get; set; }
    }
}
