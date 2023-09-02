using Learn_core_mvc.Repository.EFDBFirstRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        MyDBDbContext DbContext { get; }
        void Save();
    }
}
