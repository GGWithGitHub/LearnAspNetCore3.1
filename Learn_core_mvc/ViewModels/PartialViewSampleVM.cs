using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
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
        [StrongPassword]
        public string Password { get; set; }
    }

    public class StrongPasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var password = value.ToString();

            // Check for at least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Check for at least one lowercase letter
            if (!password.Any(char.IsLower))
            {
                return false;
            }

            // Check for at least one digit
            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            // Check for at least one special character (non-alphanumeric)
            var specialCharacterPattern = new Regex(@"[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]");
            if (!specialCharacterPattern.IsMatch(password))
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"{name} must contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
        }
    }
}
