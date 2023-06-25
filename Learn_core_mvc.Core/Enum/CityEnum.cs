using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learn_core_mvc.Core.Enum
{
    public enum CityEnum
    {
        [Display(Name = "Indore")]
        Indore_100,

        [Display(Name = "Bhopal")]
        Bhopal_101,

        [Display(Name = "Delhi")]
        Delhi_102,

        [Display(Name = "Rau")]
        Rau_103,

        [Display(Name = "Dhar")]
        Dhar_104
    }
}
