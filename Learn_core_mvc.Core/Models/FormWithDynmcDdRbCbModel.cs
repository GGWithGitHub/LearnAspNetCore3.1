using Learn_core_mvc.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class FormWithDynmcDdRbCbModel
    {
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }

        [Display(Name = "Select your city")]
        [Required]
        public CityEnum City { get; set; }

        [Display(Name = "Select your class")]
        [Required]
        public ClassEnum Class { get; set; }
    }
}
