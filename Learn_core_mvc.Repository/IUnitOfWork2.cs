using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository
{
    public interface IUnitOfWork2
    {
        IEFCoreDBFirstUowRepository2 eFCoreDBFirstUowRepository2 { get; }

        void Save();
    }
}
