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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblStudentCf>()
                .HasOne(p => p.Course)
                .WithMany(p => p.Students)
                .HasForeignKey(p => p.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull); // This will remove the ReferentialAction.Cascade behavior
        }
    }
}
