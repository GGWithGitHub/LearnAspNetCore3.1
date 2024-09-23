﻿// <auto-generated />
using System;
using Learn_core_mvc.Repository.EFCodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    [DbContext(typeof(CodeFirstDbContext))]
    partial class CodeFirstDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblCourseCf", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CourseId");

                    b.ToTable("TblCourseCf");
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblEvaluationFluentAPI", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalExplanation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("TblEvaluationFluentAPI");
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentCf", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.ToTable("TblStudentCf");
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentDetailsFluentAPI", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdditionalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("TblStudentDetailsFluentAPI");
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentFluentAPI", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<bool>("IsRegularStudent")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasName("IX_Student_Name");

                    b.ToTable("TblStudentFluentAPI");
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblEvaluationFluentAPI", b =>
                {
                    b.HasOne("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentFluentAPI", "Student")
                        .WithMany("Evaluations")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentCf", b =>
                {
                    b.HasOne("Learn_core_mvc.Repository.EFCodeFirst.Models.TblCourseCf", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentDetailsFluentAPI", b =>
                {
                    b.HasOne("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentFluentAPI", "Student")
                        .WithOne("StudentDetails")
                        .HasForeignKey("Learn_core_mvc.Repository.EFCodeFirst.Models.TblStudentDetailsFluentAPI", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
