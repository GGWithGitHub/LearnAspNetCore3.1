using Learn_core_mvc.Repository.EFCodeFirst.Configurations;
using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFCodeFirst
{
    public class CodeFirstDbContext : DbContext
    {
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options): base(options)
        {
        }

        public DbSet<TblStudentCf> TblStudentCf { get; set; }
        public DbSet<TblCourseCf> TblCourseCf { get; set; }
        public DbSet<TblStudentFluentAPI> TblStudentFluentAPI { get; set; }
        public DbSet<TblStudentDetailsFluentAPI> TblStudentDetailsFluentAPI { get; set; }
        public DbSet<TblEvaluationFluentAPI> TblEvaluationFluentAPI { get; set; }
        public DbSet<TblStudentSubjectFluentAPI> TblStudentSubjectFluentAPI { get; set; }
        public DbSet<TblSubjectFluentAPI> TblSubjectFluentAPI { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TblStudentFluentAPIConfig());
            modelBuilder.ApplyConfiguration(new TblStudentDetailsFluentAPIConfig());
            modelBuilder.ApplyConfiguration(new TblStudentSubjectFluentAPIConfig());

            modelBuilder.Entity<TblStudentCf>()
                .HasOne(p => p.Course)
                .WithMany(p => p.Students)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull); // This will remove the ReferentialAction.Cascade behavior
        }
    }
}
