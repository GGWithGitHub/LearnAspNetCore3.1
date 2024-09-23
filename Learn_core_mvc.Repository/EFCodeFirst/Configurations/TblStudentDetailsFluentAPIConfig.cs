using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Configurations
{
    public class TblStudentDetailsFluentAPIConfig : IEntityTypeConfiguration<TblStudentDetailsFluentAPI>
    {
        public void Configure(EntityTypeBuilder<TblStudentDetailsFluentAPI> builder)
        {
            builder
                .HasKey(sd => sd.Id);

            builder.Property(sd => sd.Address)
                .IsRequired(false);

            builder.Property(sd => sd.AdditionalInformation)
                .IsRequired(false);

            // Configure the foreign key relationship to Student
            builder.HasOne(sd => sd.Student)
                .WithOne(s => s.StudentDetails)
                .HasForeignKey<TblStudentDetailsFluentAPI>(sd => sd.StudentId);
        }
    }
}
