using Learn_core_mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Attributes
{
    public class AtLeastOneCheckboxCheckedAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var phoneAttributes = (List<ContactPhoneAttribute>)value;
            return phoneAttributes.Any(x=>x.IsChecked);
        }
    }
}
