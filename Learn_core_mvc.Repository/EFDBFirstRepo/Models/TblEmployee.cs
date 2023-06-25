using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblEmployee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhone { get; set; }
        public string EmpAddress { get; set; }
        public string EmpCity { get; set; }
        public string EmpState { get; set; }
        public string EmpCountry { get; set; }
    }
}
