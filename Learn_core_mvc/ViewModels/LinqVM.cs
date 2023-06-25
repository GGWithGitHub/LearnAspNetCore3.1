using Learn_core_mvc.Models;
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
}
