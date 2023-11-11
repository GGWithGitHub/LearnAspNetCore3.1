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

    public class StudentJsJqModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class DotInNameModel
    {
        public EmpTestModel Emp { get; set; }
        public StdTestModel Std { get; set; }
    }

    public class EmpTestModel
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
    }
    public class StdTestModel
    {
        public int StdId { get; set; }
        public string StdName { get; set; }
        public string StdEmail { get; set; }
    }

    public class CascadCountryModel
    {
        public string CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class CascadStateModel
    {
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CountryId { get; set; }
    }

    public class CascadCityModel
    {
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string StateId { get; set; }
    }
}
