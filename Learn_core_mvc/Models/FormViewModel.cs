using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class FormViewModel
    {
        public string UserName { get; set; }

        //[DataType(DataType.Password)]
        public string UserPassword { get; set; }

        public string UserBio { get; set; }

        public string UserCountry { get; set; }

        public List<SelectListItem> UserCountries { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("--Please Select--", ""),
            new SelectListItem("India", "Ind"),
            new SelectListItem("Pakistan", "PK"),
            new SelectListItem("France", "FR")
        };

        public List<CheckboxViewModel> UserSubjects { get; set; } = new List<CheckboxViewModel> 
        {
            new CheckboxViewModel { Id = 1, LabelName = "Physics", IsChecked = false },
            new CheckboxViewModel { Id = 2, LabelName = "Chemistry", IsChecked = false },
            new CheckboxViewModel { Id = 3, LabelName = "Mathematics", IsChecked = false },
            new CheckboxViewModel { Id = 4, LabelName = "Biology", IsChecked = false },
            new CheckboxViewModel { Id = 5, LabelName = "Commerce", IsChecked = false }
        };

        [HiddenInput]
        public string ApplicationType { get; set; } = "Online";

        public bool AcceptsTerms { get; set; }

        public string UserGender { get; set; }
        public List<SelectListItem> UserGenders { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("Male", "M"),
            new SelectListItem("Female", "F"),
            new SelectListItem("No Disclose", "N")
        };
    }

    public class CheckboxViewModel
    {
        public int Id { get; set; }
        public string LabelName { get; set; }
        public bool IsChecked { get; set; }
    }
}
