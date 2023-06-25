using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblStudent
    {
        public int StdId { get; set; }
        public string StdName { get; set; }
        public string StdRollNumber { get; set; }
        public string StdClass { get; set; }
        public string StdSubject { get; set; }
    }
}
