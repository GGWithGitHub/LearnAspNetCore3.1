using Grizzly.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Grizzly.Web.Models
{
    public class PaymentModel
    {
        public PaymentModel()
        {
            LoadCreditCardIssuers(); 
        }
 
        public Dictionary<string, int> Issuers = new Dictionary<string, int>();

        public Guid PaymentId { get; set; }

        [CreditIssuer(CreditIssuer.VISA, CreditIssuer.MASTERCARD, CreditIssuer.DISCOVER, CreditIssuer.AMEX, ErrorMessage = "Invalid Issuer")]
        public CreditIssuer Issuer { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [CreditCardNumber]
        [CreditCardNumberLength]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        [Display(Name = "Expiry Month")]
        public int ExpiryMonth { get; set; }

        [Required]
        [Range(20, 99)]
        [Display(Name = "Expiry Year")]
        public int ExpiryYear { get; set; }
 
        [Display(Name = "Security Code")]
        public string CVV { get; set; }

        [Display(Name = "Set as my default payment")]
        public bool SetAsDefault { get; set; }

        public AddressModel BillingAddress { get; set; }

        private string formAction = "NewPayment";
        public string FormAction
        {
            get
            {
                return formAction;
            }

            set
            {
                formAction = value;
            }
        }

        public CreditCardPayment ToCreditCard()
        {
            return new CreditCardPayment()
            {
                BillingAddress = BillingAddress.ToAddress(),
                CardNumber = this.CardNumber,
                CVV = this.CVV,
                ExpiryMonth = this.ExpiryMonth,
                ExpiryYear = this.ExpiryYear,
                Name = this.BillingAddress.Name,
                IsDefaultPayment = this.SetAsDefault,
                Issuer = this.Issuer,
                Last4 = this.CardNumber.Substring(Math.Max(0, this.CardNumber.Length - 4)),
                UserId = this.UserId
            };
        }

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
                    Issuers.Add(enumMember.Name, (int)Enum.Parse(typeof(CreditIssuer), enumMember.Name));
                }
            } 
        }

        public DateTime ExpireDate
        {
            get { return new DateTime(2000 + (this.ExpiryMonth < 12 ? this.ExpiryYear : this.ExpiryYear + 1), this.ExpiryMonth < 12 ? this.ExpiryMonth + 1 : 1, 1); }
        }

        public bool Expired
        {
            get { return this.ExpireDate < DateTime.UtcNow; }
        }
    }

    public class CreditCardNumberLengthAttribute : ValidationAttribute
    {
        public CreditCardNumberLengthAttribute()
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var ccNum = (string)value;

            if (String.IsNullOrWhiteSpace(ccNum))
                return new ValidationResult("error");

            int length = ccNum.Length;

            PaymentModel paymentModel = (PaymentModel)context.ObjectInstance;

            if (paymentModel.Issuer == CreditIssuer.VISA ||
                paymentModel.Issuer == CreditIssuer.MASTERCARD ||
                paymentModel.Issuer == CreditIssuer.DISCOVER
                )
            {
                if (length >= 16)
                {
                    return null;
                }
            }
            else if (paymentModel.Issuer == CreditIssuer.AMEX)
            {
                if (length >= 15)
                {
                    return null;
                }
            }
            return new ValidationResult("error");
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

    public class CreditCardNumberAttribute : ValidationAttribute
    { 

        public CreditCardNumberAttribute()
        { 
        }
         
        /// <summary>
        /// Performs the Luhn algorithm check on a card number given
        /// as a string.
        /// </summary>
        /// <param name="creditcardnumber"></param>
        /// <returns>Boolean value indicating the validity of the number</returns>
        public override bool IsValid(object value)
        {
            var ccNum = (string)value;

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
}
