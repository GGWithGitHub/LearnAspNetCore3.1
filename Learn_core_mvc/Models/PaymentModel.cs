using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class StripeUserFormModel
    {
        [Required(ErrorMessage = "FirstName is required.")]
        [MinLength(3,ErrorMessage = "FirstName should have at least 3 character.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        [MinLength(3, ErrorMessage = "LastName should have at least 3 character.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", ErrorMessage = "Password must contain at least one digit, one lowercase letter, one uppercase letter, one special character, and be at least 6 characters long.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [Compare("Password", ErrorMessage = "Password and confirm password must be same.")]
        public string ConfirmPassword { get; set; }

        public string SmsPhone { get; set; }

        [Required(ErrorMessage = "Address1 is required.")]
        public string Address1 { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }
        public int SubscriptionTypeId { get; set; }
    }
}
