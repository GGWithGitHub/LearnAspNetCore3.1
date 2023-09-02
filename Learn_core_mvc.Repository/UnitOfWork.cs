using Learn_core_mvc.Repository.EFDBFirstRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBDbContext _dbContext;

        public UnitOfWork(MyDBDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MyDBDbContext DbContext => _dbContext;

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
