using Learn_core_mvc.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class ContactVM
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public int PhoneAttributeId { get; set; }
        public string PhoneAttributeName { get; set; }
        public List<Contact> GetContacts()
        {
            return new List<Contact>() {
                new Contact(){ Id=1001, Name="Aanil", Email="aanil@gmail.com"},
                new Contact(){ Id=1002, Name="Manil", Email="manil@gmail.com"},
                new Contact(){ Id=1003, Name="Sunil", Email="sunil@gmail.com"},
            };
        }
        public List<ContactPhone> GetPhones()
        {
            return new List<ContactPhone>() {
                new ContactPhone(){ Id=1, Number="5847589658", ContactId=1001},
                new ContactPhone(){ Id=2, Number="6847589658", ContactId=1001},
                new ContactPhone(){ Id=3, Number="7847589658", ContactId=1002},
                new ContactPhone(){ Id=4, Number="8847589658", ContactId=1002},
                new ContactPhone(){ Id=5, Number="9847589658", ContactId=1003}
            };
        }
        public List<MapContactPhoneAndPhoneAttribute> GetMapPhoneAndPhoneAttributes()
        {
            return new List<MapContactPhoneAndPhoneAttribute>()
            {
                new MapContactPhoneAndPhoneAttribute(){ Id=201, PhoneId=1, PhoneAttributeId=101},
                new MapContactPhoneAndPhoneAttribute(){ Id=202, PhoneId=1, PhoneAttributeId=102},
                new MapContactPhoneAndPhoneAttribute(){ Id=203, PhoneId=1, PhoneAttributeId=103},
                new MapContactPhoneAndPhoneAttribute(){ Id=204, PhoneId=2, PhoneAttributeId=104},
                new MapContactPhoneAndPhoneAttribute(){ Id=205, PhoneId=3, PhoneAttributeId=105},
            };
        }
        public List<ContactPhoneAttribute> GetPhoneAttributes()
        {
            return new List<ContactPhoneAttribute>() {
                new ContactPhoneAttribute(){ Id=101, Name="24 Hrs", IsChecked=false},
                new ContactPhoneAttribute(){ Id=102, Name="Fax", IsChecked=false},
                new ContactPhoneAttribute(){ Id=103, Name="Landline", IsChecked=false},
                new ContactPhoneAttribute(){ Id=104, Name="SMS", IsChecked=false},
                new ContactPhoneAttribute(){ Id=105, Name="MMS", IsChecked=false},
            };
        }

    }
    public class AddEditContactVM
    {
        //public Contact Contact { get; set; }
        //public List<Contact> Contacts { get; set; }
        //public List<ContactPhone> Phones { get; set; }
        //public List<ContactPhoneAttribute> PhoneAttributes { get; set; }
        //public List<MapContactPhoneAndPhoneAttribute> MapPhoneAndPhoneAttributes { get; set; }

        public int ContactId { get; set; }

        [Required]
        public string ContactName { get; set; }

        [Required]
        public string ContactEmail { get; set; }
        public List<PhoneWithPhoneAttributes> PhoneWithPhoneAttributes { get; set; } = new List<PhoneWithPhoneAttributes>();
    }

    public class ContactUsVM
    {
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter message")]
        public string Message { get; set; }
        public string GoogleCaptchaToken { get; set; }
    }

    public class CaptchaResponseVM
    {
        public bool Success { get; set; }

        [JsonProperty(PropertyName = "error-codes")]
        public IEnumerable<string> ErrorCodes { get; set; }

        [JsonProperty(PropertyName = "challenge_ts")]
        public DateTime ChallengeTime { get; set; }

        public string HostName { get; set; }
        public double Score { get; set; }
        public string Action { get; set; }
    }
}
