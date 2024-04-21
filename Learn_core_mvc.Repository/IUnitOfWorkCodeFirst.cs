using Learn_core_mvc.Repository.EFCodeFirst;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository
{
    public interface IUnitOfWorkCodeFirst : IDisposable
    {
        CodeFirstDbContext DbContext { get; }
        void Save();
    }
}
