using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public interface IEFCoreDBFirstUowRepository
    {
        Task<List<TblEmployee>> GetEmployees();
        Task<TblEmployee> GetEmployeeById(int empId);
        Task<bool> DeleteEmployee(int empId);
        Task<bool> CreateEmployee(TblEmployee emp);
        Task<bool> UpdateEmployee(TblEmployee emp);

        Task<List<EmpSpDbFirstRepoUowModel>> GetEmployeesSP();
        Task<EmpSpDbFirstRepoUowModel> GetEmployeeByIdSP(int empId);
        Task<bool> CreateEmployeeSp(EmpSpDbFirstRepoUowModel emp);
        Task<bool> UpdateEmployeeSp(EmpSpDbFirstRepoUowModel emp);
        Task<bool> DeleteEmployeeByIdSp(int empId);
    }
}
