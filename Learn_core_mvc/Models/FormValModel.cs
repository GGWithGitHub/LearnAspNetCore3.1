using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class FormValModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please choose gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please choose subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateofBirth { get; set; }

        [Required(ErrorMessage = "Choose batch time")]
        [Display(Name = "Batch Time")]
        [DataType(DataType.Time)]
        public DateTime BatchTime { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        [Remote(action: "CheckEmailExist", controller: "Form")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter website url")]
        [Display(Name = "Website Url")]
        [Url]
        public string WebSite { get; set; }

        [Required]
        [CreditCardNumber(ErrorMessage = "Please enter a valid card number")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StrongPassword]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
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

    public class CreditCardNumberAttribute : ValidationAttribute
    {
        public CreditCardNumberAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var ccNum = (string)value;

            bool isValid = true;
            int length = ccNum.Length;

            if (length < 13)
            {
                isValid = false;
            }
            else
            {
                int sum = 0;
                int offset = length % 2;
                byte[] digits = new System.Text.ASCIIEncoding().GetBytes(ccNum);

                for (int i = 0; i < length; i++)
                {
                    digits[i] -= 48;
                    if (((i + offset) % 2) == 0)
                        digits[i] *= 2;

                    sum += (digits[i] > 9) ? digits[i] - 9 : digits[i];
                }

                isValid = ((sum % 10) == 0);
            }

            return isValid;
        }
    }
}
