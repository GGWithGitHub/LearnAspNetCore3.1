using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFDBFirstRepo
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
