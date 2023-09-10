using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo.SPModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository.EFDBFirstRepo
{
    public partial class MyDBDbContext
    {
        public DbSet<GetEmpSpModel> GetEmpsBySp { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetEmpSpModel>().HasNoKey();
        }
    }
}
