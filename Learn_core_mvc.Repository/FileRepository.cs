using Learn_core_mvc.Repository.EFCodeFirst;
using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly CodeFirstDbContext _dbcontext;
        public FileRepository(CodeFirstDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<TblFile>> GetAll()
        { 
            return await _dbcontext.TblFile.ToListAsync();
        }

        public async Task<TblFile> GetFile(string Id)
        {
            return await _dbcontext.TblFile.Where(c => c.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddFile(TblFile file)
        {
            try
            {
                file.Id = Guid.NewGuid().ToString();
                _dbcontext.TblFile.Add(file);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
