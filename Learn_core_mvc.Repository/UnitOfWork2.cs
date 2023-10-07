using Learn_core_mvc.Repository.EFDBFirstRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository
{
    public class UnitOfWork2 : IUnitOfWork2
    {
        private readonly MyDBDbContext _dbContext;
        public IEFCoreDBFirstUowRepository2 eFCoreDBFirstUowRepository2 { get; private set; }
        public UnitOfWork2(MyDBDbContext dbContext)
        {
            _dbContext = dbContext;
            eFCoreDBFirstUowRepository2 = new EFCoreDBFirstUowRepository2(_dbContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
