using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Learn_core_mvc.Attributes
{
    //public class ConditionalValidationAttribute : ValidationAttribute
    //{
    //    private readonly string _otherPropertyName;

    //    public ConditionalValidationAttribute(string otherPropertyName)
    //    {
    //        _otherPropertyName = otherPropertyName;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

    //        if (otherPropertyInfo == null)
    //        {
    //            throw new ArgumentException($"Property with name {_otherPropertyName} not found on object {validationContext.ObjectType.Name}");
    //        }

    //        var otherPropertyValue = (bool)otherPropertyInfo.GetValue(validationContext.ObjectInstance);

    //        if (otherPropertyValue == false)
    //        {
    //            return ValidationResult.Success;
    //        }
    //    }
    //}
}
