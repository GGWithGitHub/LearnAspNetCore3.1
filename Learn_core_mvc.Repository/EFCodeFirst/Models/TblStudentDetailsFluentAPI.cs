using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Models
{
    public class TblStudentDetailsFluentAPI
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }

        public Guid StudentId { get; set; }
        public TblStudentFluentAPI Student { get; set; }
    }
}
