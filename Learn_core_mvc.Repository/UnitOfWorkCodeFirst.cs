using Learn_core_mvc.Repository.EFCodeFirst;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository
{
    public class UnitOfWorkCodeFirst : IUnitOfWorkCodeFirst
    {
        private readonly CodeFirstDbContext _dbContext;

        public UnitOfWorkCodeFirst(CodeFirstDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CodeFirstDbContext DbContext => _dbContext;

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
