using Learn_core_mvc.Repository.EFCodeFirst;
using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public class EFCoreCodeFirstRepository : IEFCoreCodeFirstRepository
    {
        private readonly CodeFirstDbContext _dbContext;
        private readonly IUnitOfWorkCodeFirst _unitOfWork;
        public EFCoreCodeFirstRepository(CodeFirstDbContext dbContext, IUnitOfWorkCodeFirst unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TblStudentCf>> GetStudents()
        {
            var students = new List<TblStudentCf>();
            students = await _dbContext.TblStudentCf.ToListAsync();
            return students;
        }

        public async Task<TblStudentCf> GetStudentById(int stdId)
        {
            var student = new TblStudentCf();
            student = await _dbContext.TblStudentCf.Where(x => x.StudentId == stdId).FirstOrDefaultAsync();
            return student;
        }

        public async Task<bool> DeleteStudent(int stdId)
        {
            bool isSuccessful = false;
            var std = await _dbContext.TblStudentCf.Where(x => x.StudentId == stdId).FirstOrDefaultAsync();
            if (std != null)
            {
                _dbContext.TblStudentCf.Remove(std);
                _unitOfWork.Save();
                isSuccessful = true;
            }
            return isSuccessful;
        }

        public async Task<bool> CreateStudent(TblStudentCf std)
        {
            bool isSuccessful = false;
            try
            {
                await _dbContext.TblStudentCf.AddAsync(std);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            isSuccessful = true;
            return isSuccessful;
        }

        public async Task<bool> UpdateStudent(TblStudentCf std)
        {
            bool isSuccessful = false;
            var student = await _dbContext.TblStudentCf.Where(x => x.StudentId == std.StudentId).FirstOrDefaultAsync();
            if (student != null)
            {
                student.Name = std.Name;
                _unitOfWork.Save();

                isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
