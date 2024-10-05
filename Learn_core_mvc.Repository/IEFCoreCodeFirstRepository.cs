using Learn_core_mvc.Repository.EFCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public interface IEFCoreCodeFirstRepository
    {
        Task<List<TblStudentCf>> GetStudents();
        Task<TblStudentCf> GetStudentById(int stdId);
        Task<bool> DeleteStudent(int stdId);
        Task<bool> CreateStudent(TblStudentCf std);
        Task<bool> UpdateStudent(TblStudentCf std);
        Task<List<TblStudentFluentAPI>> GetTblStudentFluentAPI();
        Task<List<TblStudentDetailsFluentAPI>> GetTblStudentDetailsFluentAPI();
        Task<List<TblEvaluationFluentAPI>> GetTblEvaluationFluentAPI();
        Task<List<TblSubjectFluentAPI>> GetTblSubjectFluentAPI();
        Task<List<TblStudentSubjectFluentAPI>> GetTblStudentSubjectFluentAPI();
        Task<List<TblStudentFluentAPI>> GetOneToOneData();
        Task<bool> AddMainEntityWithRelatedEntity(TblStudentFluentAPI student);
    }
}
