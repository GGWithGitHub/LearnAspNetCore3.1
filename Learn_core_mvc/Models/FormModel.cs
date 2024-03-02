using Learn_core_mvc.Attributes;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
}
