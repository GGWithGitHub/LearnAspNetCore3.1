using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Configurations
{
    public class TblStudentSubjectFluentAPIConfig : IEntityTypeConfiguration<TblStudentSubjectFluentAPI>
    {
        public void Configure(EntityTypeBuilder<TblStudentSubjectFluentAPI> builder)
        {
            // Configure Many-to-Many relationship
            builder.HasKey(s => new { s.StudentId, s.SubjectId });
            builder.HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);
            builder.HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }
}
