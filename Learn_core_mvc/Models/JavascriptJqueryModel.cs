using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class EmployeeJsJqModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class DepartmentJsJqModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class GamesJsJqModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchDrpDwnModel
    {
        public string UserCountry { get; set; }
        public List<SelectListItem> UserCountries { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("--Please Select--", ""),
            new SelectListItem("India", "IND"),
            new SelectListItem("Pakistan", "PK"),
            new SelectListItem("France", "FR"),
            new SelectListItem("Afghanistan", "AFG"),
            new SelectListItem("Australia", "AUS"),
            new SelectListItem("Bangladesh", "BNG"),
            new SelectListItem("Belgium", "BEL"),
            new SelectListItem("Brazil", "BRZ"),
            new SelectListItem("Canada", "CND"),
            new SelectListItem("China", "CHN"),
            new SelectListItem("Colombia", "COL"),
            new SelectListItem("Denmark", "DEN"),
            new SelectListItem("Egypt", "EGPT"),
            new SelectListItem("Georgia", "GEO"),
            new SelectListItem("Italy", "ITL"),
        };
    }
}
