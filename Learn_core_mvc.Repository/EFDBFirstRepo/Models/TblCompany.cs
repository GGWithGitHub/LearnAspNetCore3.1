using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblCompany
    {
        public int CmpId { get; set; }
        public string CmpName { get; set; }
        public string CmpAddress { get; set; }
        public int? CmpRating { get; set; }
    }
}
