using Learn_core_mvc.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn_core_mvc.Service
{
    public interface IEFCoreDBFirstService
    {
        Task<List<EmployeeForEFCoreDbFirst>> GetEmployees();
        Task<EmployeeForEFCoreDbFirst> GetEmployeeById(int empId);
        Task<bool> DeleteEmployee(int empId);
        Task<bool> CreateEmployee(EmployeeForEFCoreDbFirst emp);
        Task<bool> UpdateEmployee(EmployeeForEFCoreDbFirst emp);
    }
}