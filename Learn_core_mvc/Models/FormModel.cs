using Learn_core_mvc.Attributes;
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
}
