using Learn_core_mvc.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class ContactPhone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int ContactId { get; set; }
    }
    public class ContactPhoneAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
    public class MapContactPhoneAndPhoneAttribute
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public int PhoneAttributeId { get; set; }
    }

    public class PhoneWithPhoneAttributes
    {
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        [AtLeastOneCheckboxChecked(ErrorMessage = "Please select at least one option.")]
        public List<ContactPhoneAttribute> ContactPhoneAttributes { get; set; } = new List<ContactPhoneAttribute>();
    }

    public class MultiSelect
    {
        public List<string> MultiLanguages { get; set; }
    }

    public class RemoveServerValidationModel
    {
        [Required]
        public string CardNumber { get; set; }

        [Required]
        public int CVV { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool NeedCard { get; set; }

    }

    public class ListWithMultiControlsModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserCountryCode { get; set; }
        //public List<ListWithMultiControlDD> UserCountries { get; set; }
        public List<SelectListItem> UserCountries { get; set; }

        public string UserGenderCode { get; set; }
        public List<SelectListItem> UserGenders { get; set; }
        public List<ListWithMultiControlCB> UserHobbies { get; set; }
    }
    public class ListWithMultiControlDD
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }

    public class ListWithMultiControlCB
    {
        public int HobbyId { get; set; }
        public string HobbyLabel { get; set; }
        public bool IsChecked { get; set; }
    }

    public class CustRegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Enter phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage ="Enter email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }

    #region Payment related class, attribute, enum

    public enum CreditIssuer
    {
        [Payment("NONE", "NON", false)]
        NONE,

        [Payment("Visa", "VIS", true)]
        VISA,

        [Payment("MasterCard", "MAS", true)]
        MASTERCARD,

        [Payment("Discover", "DIS", true)]
        DISCOVER,

        [Payment("American Express", "AME", true)]
        AMEX,

        [Payment("PayPal&reg;", "PAY", false)]
        PAYPAL,

        [Payment("Amazon Login and Pay", "APY", false)]
        AMAZON,

        [Payment("UPS i-parcel", "IPA", false)]
        IPARCEL,

        [Payment("Affirm", "AFF", false)]
        AFFIRM,

        [Payment("Online Quote", "PRO", false)]
        QUOTE
    }

    public class PaymentModel
    {
        public PaymentModel()
        {
            LoadCreditCardIssuers();
        }

        public Dictionary<string, int> Issuers = new Dictionary<string, int>();

        [CreditIssuer(CreditIssuer.VISA, CreditIssuer.MASTERCARD, CreditIssuer.DISCOVER, CreditIssuer.AMEX, ErrorMessage = "Invalid Issuer")]
        public CreditIssuer Issuer { get; set; }

        [Display(Name = "Card Holder Name")]
        [Required(ErrorMessage = "Card holder name is required")]
        public string CardHolderName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [CCNumber(ErrorMessage = "Card number is not valid")]
        [CreditCardNumberLength]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; }

        //[Required]
        //[Range(1, 12)]
        //[Display(Name = "Expiry Month")]
        //public int ExpiryMonth { get; set; }

        //[Required]
        //[Range(20, 99)]
        //[Display(Name = "Expiry Year")]
        //public int ExpiryYear { get; set; }

        [Required(ErrorMessage = "Expire date is required")]
        [Display(Name = "Expire Date")]
        [ExpireDate(ErrorMessage = "Invalid expire date")]
        public string ExpireDate { get; set; }

        [Required(ErrorMessage = "CVV is required")]
        [Display(Name = "Security Code")]
        public string CVV { get; set; }

        private void LoadCreditCardIssuers()
        {
            var enumType = typeof(CreditIssuer);

            foreach (MemberInfo enumMember in enumType.GetMembers())
            {
                var valueAttributes =
                      enumMember.GetCustomAttributes(typeof(PaymentAttribute), false);

                if (valueAttributes == null || valueAttributes.Length == 0)
                    continue;

                PaymentAttribute paymentAttr = ((PaymentAttribute)valueAttributes[0]);

                if (paymentAttr.IsCreditCard)
                {
                    Issuers.Add(enumMember.Name,
                        (int)Enum.Parse(typeof(CreditIssuer), enumMember.Name)
                    );
                }
            }
        }
    }

    public class ExpireDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var input = (string)value;
            var parts = input.Split('/');
            var month = Convert.ToInt32(parts[0], 10);
            var year = Convert.ToInt32(parts[1], 10);
            var currentDate = DateTime.Now;
            var currentYear = currentDate.Year % 100; // Get last two digits of current year
            var currentMonth = currentDate.Month + 1; // Get current month (0-based index)

            if (year > currentYear || (year == currentYear && month >= currentMonth))
            {
                // Valid expiry date
                return true;
            }
            else
            {
                // Invalid expiry date
                return false;
            }
        }
    }


    public class CreditCardNumberLengthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            //var ccNum = (string)value;
            var ccNum = Regex.Replace((string)value, @"\s+", ""); ;

            if (String.IsNullOrWhiteSpace(ccNum))
                return new ValidationResult("Card number length is not valid");

            int length = ccNum.Length;

            PaymentModel paymentModel = (PaymentModel)context.ObjectInstance;

            if (paymentModel.Issuer == CreditIssuer.VISA ||
                paymentModel.Issuer == CreditIssuer.MASTERCARD ||
                paymentModel.Issuer == CreditIssuer.DISCOVER
                )
            {
                if (paymentModel.Issuer == CreditIssuer.VISA && ccNum.StartsWith('4'))
                {
                    if (length >= 16)
                    {
                        return null;
                    }
                }
                else if (paymentModel.Issuer == CreditIssuer.MASTERCARD && ccNum.StartsWith('5'))
                {
                    if (length >= 16)
                    {
                        return null;
                    }
                }
                else if (paymentModel.Issuer == CreditIssuer.DISCOVER &&
                    (ccNum.StartsWith("6011") || ccNum.StartsWith("622126") || ccNum.StartsWith("622") ||
                    ccNum.StartsWith("644") || ccNum.StartsWith("645") || ccNum.StartsWith("646") ||
                    ccNum.StartsWith("647") || ccNum.StartsWith("648") || ccNum.StartsWith("649"))
                )
                {
                    if (length >= 16)
                    {
                        return null;
                    }
                }
            }
            else if (paymentModel.Issuer == CreditIssuer.AMEX && (ccNum.StartsWith("34") || ccNum.StartsWith("37")))
            {
                if (length >= 15)
                {
                    return null;
                }
            }
            return new ValidationResult("Card number length of issuer is not valid");
        }
    }

    public class CreditIssuerAttribute : ValidationAttribute
    {
        private CreditIssuer[] validCreditIssuers;

        public CreditIssuerAttribute(params CreditIssuer[] validCreditIssuers)
        {
            this.validCreditIssuers = validCreditIssuers;
        }

        public override bool IsValid(object value)
        {
            return validCreditIssuers.Contains((CreditIssuer)value);
        }
    }

    public class CCNumberAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //var ccNum = (string)value;
            var ccNum = Regex.Replace((string)value, @"\s+", "");

            if (String.IsNullOrWhiteSpace(ccNum))
                return false;

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

    public class PaymentAttribute : Attribute
    {
        private bool isCreditCard;
        private string description;
        private string opshortcode;

        public PaymentAttribute()
        {
            description = "";
            opshortcode = "";
            isCreditCard = false;
        }

        public PaymentAttribute(string paymentDescription, string shortCode, bool isCard)
        {
            description = paymentDescription;
            isCreditCard = isCard;
            opshortcode = shortCode;
        }

        public string Description
        {
            get { return description; }
        }

        public bool IsCreditCard
        {
            get { return isCreditCard; }
        }

        public string OPShortCode
        {
            get { return opshortcode; }
        }

    }

    #endregion

}
