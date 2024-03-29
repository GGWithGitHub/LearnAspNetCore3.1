﻿using Learn_core_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class LinqInnerJoinVM
    {
        public List<LinqBook> BookProperty { get; set; }
        public List<LinqOrder> OrderProperty { get; set; }
        public List<LinqBookOrder> BookOrderProperty { get; set; }
    }

    public class LinqInnerJoinMulTblVM
    {
        public List<LinqDepartment> DepartmentsProperty { get; set; }
        public List<LinqEmployee> EmployeesProperty { get; set; }
        public List<LinqAddress> AddressesProperty { get; set; }
        public List<LinqCountry> CountriesProperty { get; set; }
        public List<LinqDepEmpAddrCoun> DepEmpAddrCounProperty { get; set; }
    }

    public class LinqLeftJoinVM
    {
        public List<LinqBook> BookProperty { get; set; }
        public List<LinqOrder> OrderProperty { get; set; }
        public List<LinqBookOrder> BookOrderProperty { get; set; }
    }

    public class LinqGroupJoinVM
    {
        public List<LinqDepartment> DepartmentsProperty { get; set; }
        public List<LinqEmployee> EmployeesProperty { get; set; }
        public List<LinqDepartmentEmployee> DepartmentEmployeeProperty { get; set; }
    }

    public class LinqSortVM
    {
        public List<LinqSortModel> SortProperty { get; set; }
    }

    public class LinqPersonVM
    {
        public List<LinqPerson> PersonProperty { get; set; }
    }

    public class LinqAuthorVM
    {
        public List<LinqAuthor> AuthorProperty { get; set; }
        public List<LinqAuthorsBook> AuthorsBookProperty { get; set; }
    }
}
