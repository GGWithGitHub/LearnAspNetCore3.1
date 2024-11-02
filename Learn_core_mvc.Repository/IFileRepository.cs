using Learn_core_mvc.Repository.EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public interface IFileRepository
    {
        Task<bool> AddFile(TblFile file);
        Task<IEnumerable<TblFile>> GetAll();
        Task<TblFile> GetFile(string Id);
    }
}
