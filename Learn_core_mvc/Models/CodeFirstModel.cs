using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class StudentFluentAPIModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public bool? IsRegularStudent { get; set; }
    }

    public class StudentDetailsFluentAPIModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }
        public Guid StudentId { get; set; }
    }

    public class StdStdDetailsFluentAPIModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public bool IsRegularStudent { get; set; }
        public StudentDetailsFluentAPIModel StudentDetails { get; set; }
    }

    public class StdDetailsStdFluentAPIModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string AdditionalInformation { get; set; }
        public StudentFluentAPIModel Student { get; set; }
    }

    public class EvaluationFluentAPIModel
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public string AdditionalExplanation { get; set; }

        public Guid StudentId { get; set; }
    }

    public class StudentSubjectFluentAPIModel
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
    }

    public class SubjectFluentAPIModel
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
    }
}

