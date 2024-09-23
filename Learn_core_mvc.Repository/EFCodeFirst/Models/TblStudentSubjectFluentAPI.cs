using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Models
{
    public class TblStudentSubjectFluentAPI
    {
        public Guid StudentId { get; set; }
        public TblStudentFluentAPI Student { get; set; }
        public Guid SubjectId { get; set; }
        public TblSubjectFluentAPI Subject { get; set; }
    }
}
