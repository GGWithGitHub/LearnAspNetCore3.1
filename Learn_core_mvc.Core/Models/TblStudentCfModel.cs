using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class TblStudentCfModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? CourseId { get; set; }
    }
}
