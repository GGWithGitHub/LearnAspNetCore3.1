using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblDatatableEmp
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpGender { get; set; }
        public decimal? EmpSalary { get; set; }
        public string EmpCity { get; set; }
    }
}
