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
    }
}
