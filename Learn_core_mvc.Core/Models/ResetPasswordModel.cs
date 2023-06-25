using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class ResetPasswordModel
    {
        public Guid Token { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        //[MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")] // it is true statement
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string PasswordConfirmation { get; set; }
    }
}
