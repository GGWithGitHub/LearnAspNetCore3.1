using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst.Configurations
{
    public class TblStudentFluentAPIConfig : IEntityTypeConfiguration<TblStudentFluentAPI>
    {
        public void Configure(EntityTypeBuilder<TblStudentFluentAPI> builder)
        {
            // Configure the primary key for the TblStudentFluentAPI entity
            builder.HasKey(s => s.Id);

            // Configure the Name property
            builder.Property(s => s.Name)
                .IsRequired() // The Name property is required (not nullable)
                .HasMaxLength(50); // Maximum length of 50 characters

            // Add an index on the Name property
            builder.HasIndex(s => s.Name)
                .HasName("IX_Student_Name") // Optional: specify the index name
                .IsUnique(false); // Set to true if you want a unique index (default is false)

            // Configure the Age property
            builder.Property(s => s.Age)
                .IsRequired(false); // The Age property is optional (nullable)

            // Configure the IsRegularStudent property
            builder.Property(s => s.IsRegularStudent)
                .HasDefaultValue(true); // Set a default value of true for the IsRegularStudent property

            // Configure one-to-one relationship with StudentDetails
            builder.HasOne(s => s.StudentDetails)
                .WithOne(sd => sd.Student)
                .HasForeignKey<TblStudentDetailsFluentAPI>(sd => sd.StudentId);

            // Configure One-to-Many relationship with Evaluations
            builder.HasMany(e => e.Evaluations)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId);
        }
    }
}
