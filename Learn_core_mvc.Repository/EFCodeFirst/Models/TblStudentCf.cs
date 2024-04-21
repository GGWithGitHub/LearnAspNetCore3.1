using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Models
{
    public class TblStudentCf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataType(DataType.Date),
         DisplayFormat(DataFormatString = "{0:dd/MM/yy}",
         ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(50)]
        public string? EmailId { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        public int? CourseId { get; set; }
        public TblCourseCf Course { get; set; } // Navigation Property  
    }
}
