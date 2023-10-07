using Learn_core_mvc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Service
{
    public interface IEFCoreDBFirstRepoUowService
    {
        Task<List<EmpDbFirstRepoUowModel>> GetEmployees();
        Task<EmpDbFirstRepoUowModel> GetEmployeeById(int empId);
        Task<bool> DeleteEmployee(int empId);
        Task<bool> CreateEmployee(EmpDbFirstRepoUowModel emp);
        Task<bool> UpdateEmployee(EmpDbFirstRepoUowModel emp);

        Task<List<EmpSpDbFirstRepoUowModel>> GetEmployeesSP();
        Task<EmpSpDbFirstRepoUowModel> GetEmployeeSP(int empId);
        Task<bool> DeleteEmployeeSP(int empId);
        Task<bool> CreateEmployeeSP(EmpSpDbFirstRepoUowModel emp);
        Task<bool> UpdateEmployeeSP(EmpSpDbFirstRepoUowModel emp);

        Task<List<EmpDbFirstRepoUowModel>> GetEmployees2();
        Task<EmpDbFirstRepoUowModel> GetEmployeeById2(int empId);
        Task<bool> DeleteEmployee2(int empId);
        Task<bool> CreateEmployee2(EmpDbFirstRepoUowModel emp);
        Task<bool> UpdateEmployee2(EmpDbFirstRepoUowModel emp);
    }
}
