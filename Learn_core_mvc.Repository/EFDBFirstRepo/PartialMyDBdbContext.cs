using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo.SPModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var softDeleteEntities = ChangeTracker.Entries<ISoftDelete>().Where(x => x.State == EntityState.Deleted).ToList();
            foreach (var entry in softDeleteEntities)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
            }
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            return result;
        }
    }
}
