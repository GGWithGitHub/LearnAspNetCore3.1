using Learn_core_mvc.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    //public interface ISampleSqlRepository<T>
    public interface ISampleSqlRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<bool> DeleteEmployee(int id);
        Task<bool> CreateEmployee(Employee emp);
        Task<bool> UpdateEmployee(Employee emp);
        Task<Employee> GetEmployeeByIdBySp(int id);
        Task<List<Employee>> GetEmployeesBySp();
        Task<bool> CreateEmployeeBySp(Employee emp);
        Task<bool> UpdateEmployeeBySp(Employee emp);
        Task<bool> DeleteEmployeeByIdBySp(int id);
        Task<List<Company>> GetCompanies();
        Task<Login> LoginUser(string email);
        Task<Login> GetUserByEmail(string email);
        Task<bool> RegisterUser(Login user);
        Task<List<DatatableEmp>> GetDTEmpsLimit(DatatableProperties dtProp);
        Task<int> GetDTTotalEmpsCount();
        Task<int> GetDTTotalFilterEmpsCount(string searchValue);
        Task InsertException(ExceptionLogger exceptionLogger);
        Task<bool> UpdateUserToken(long userId, Guid token);
        Task<Login> GetUserByToken(Guid token);
        Task<bool> UpdateUserPassword(long userId, string password);
    }
}
