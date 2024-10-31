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
                student.DateOfBirth = std.DateOfBirth;
                student.EmailId = std.EmailId;
                student.Address = std.Address;
                student.City = std.City;
                _unitOfWork.Save();

                isSuccessful = true;
            }
            return isSuccessful;
        }

        public async Task<List<TblStudentFluentAPI>> GetTblStudentFluentAPI()
        {
            var students = new List<TblStudentFluentAPI>();
            students = await _dbContext.TblStudentFluentAPI.ToListAsync();
            return students;
        }

        public async Task<List<TblStudentDetailsFluentAPI>> GetTblStudentDetailsFluentAPI()
        {
            var studentDetails = new List<TblStudentDetailsFluentAPI>();
            studentDetails = await _dbContext.TblStudentDetailsFluentAPI.ToListAsync();
            return studentDetails;
        }

        public async Task<List<TblEvaluationFluentAPI>> GetTblEvaluationFluentAPI()
        {
            var evalutions = new List<TblEvaluationFluentAPI>();
            evalutions = await _dbContext.TblEvaluationFluentAPI.ToListAsync();
            return evalutions;
        }

        public async Task<List<TblSubjectFluentAPI>> GetTblSubjectFluentAPI()
        {
            var subjects = new List<TblSubjectFluentAPI>();
            subjects = await _dbContext.TblSubjectFluentAPI.ToListAsync();
            return subjects;
        }

        public async Task<List<TblStudentSubjectFluentAPI>> GetTblStudentSubjectFluentAPI()
        {
            var stdSubjects = new List<TblStudentSubjectFluentAPI>();
            stdSubjects = await _dbContext.TblStudentSubjectFluentAPI.ToListAsync();
            return stdSubjects;
        }

        public async Task<List<TblStudentDetailsFluentAPI>> GetOneToOneData()
        {
            var stdStdDetail = await (from stdDet in _dbContext.TblStudentDetailsFluentAPI
                                      join std in _dbContext.TblStudentFluentAPI.AsNoTracking()
                                      on stdDet.StudentId equals std.Id into studentGroup
                                      from std in studentGroup.DefaultIfEmpty() // Left join
                                      select new TblStudentDetailsFluentAPI
                                      {
                                          Id = stdDet.Id,
                                          Address = stdDet.Address,
                                          AdditionalInformation = stdDet.AdditionalInformation,
                                          Student = new TblStudentFluentAPI
                                          {
                                              Id = std.Id,
                                              Age = std.Age,
                                              Name = std.Name,
                                              IsRegularStudent = std.IsRegularStudent
                                          }
                                      }).ToListAsync();

            return stdStdDetail;
        }

        public async Task<bool> AddMainEntityWithRelatedEntity(TblStudentFluentAPI student)
        {
            student.Id = Guid.NewGuid();
            student.StudentDetails.Id = Guid.NewGuid();

            _dbContext.TblStudentFluentAPI.Add(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DelMainEntity(Guid studentId)
        {
            var student = new TblStudentFluentAPI();
            student = await _dbContext.TblStudentFluentAPI.Where(x => x.Id == studentId).FirstOrDefaultAsync();
            if (student != null)
            {
                _dbContext.TblStudentFluentAPI.Remove(student);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
