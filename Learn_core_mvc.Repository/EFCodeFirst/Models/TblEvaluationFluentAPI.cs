using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Models
{
    public class TblEvaluationFluentAPI
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public string AdditionalExplanation { get; set; }

        public Guid StudentId { get; set; }
        public TblStudentFluentAPI Student { get; set; }
    }
}
