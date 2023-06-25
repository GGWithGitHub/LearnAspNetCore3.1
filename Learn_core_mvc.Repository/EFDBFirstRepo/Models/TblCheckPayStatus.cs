using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblCheckPayStatus
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public decimal? EmpSalary { get; set; }
        public string PaidStatus { get; set; }
    }
}
