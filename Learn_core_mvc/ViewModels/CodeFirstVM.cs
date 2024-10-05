using Learn_core_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class FluentAPIVM
    {
        public List<StudentFluentAPIModel> ListStudent { get; set; }
        public List<StudentDetailsFluentAPIModel> ListStudentDetail { get; set; }
        public List<EvaluationFluentAPIModel> ListEvaluation { get; set; }
        public List<StudentSubjectFluentAPIModel> ListStudentSubject { get; set; }
        public List<SubjectFluentAPIModel> ListSubject { get; set; }
        public List<StdStdDetailsFluentAPIModel> ListStdStdDetail { get; set; }
    }
}
