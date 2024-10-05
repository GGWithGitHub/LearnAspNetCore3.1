using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Models
{
    public class TblStudentFluentAPI
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public bool? IsRegularStudent { get; set; }
        public TblStudentDetailsFluentAPI StudentDetails { get; set; }
        public ICollection<TblEvaluationFluentAPI> Evaluations { get; set; }
        public ICollection<TblStudentSubjectFluentAPI> StudentSubjects { get; set; }
    }
}
