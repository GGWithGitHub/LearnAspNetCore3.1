using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Models
{
    public class TblSubjectFluentAPI
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }

        public ICollection<TblStudentSubjectFluentAPI> StudentSubjects { get; set; }
    }
}
