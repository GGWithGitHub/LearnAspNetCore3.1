using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public interface ICacheRepository<T>
    {
        Task<bool> KeyExists(string key);
        Task<bool> Delete(string key);
        Task<bool> Save(string key, T obj);
        Task<T> Retrieve(string key);
    }
}
