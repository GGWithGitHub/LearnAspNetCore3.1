using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class EmpSpDbFirstRepoUowModel
    {
        public int EmpId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string EmpName { get; set; }

        [Display(Name = "Email")]
        public string EmpEmail { get; set; }

        [Display(Name = "Phone")]
        public string EmpPhone { get; set; }

        [Display(Name = "Address")]
        public string EmpAddress { get; set; }

        [Display(Name = "City")]
        public string EmpCity { get; set; }

        [Display(Name = "State")]
        public string EmpState { get; set; }

        [Display(Name = "Country")]
        public string EmpCountry { get; set; }
    }
}
