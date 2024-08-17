using Learn_core_mvc.Core.Models;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Service
{
    public interface ISampleService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<bool> UpdateEmployee(Employee emp);
        Task<bool> DeleteEmployee(int id);
        Task<bool> CreateEmployee(Employee emp);
        Task<Employee> GetEmployeeByIdBySp(int id);
        Task<List<Employee>> GetEmployeesBySp();
        Task<bool> CreateEmployeeBySp(Employee emp);
        Task<List<Employee>> CreateMultiEmployeesBySp(List<Employee> emps);
        Task<List<Employee>> UpdateMultiEmployeesBySp(List<Employee> emps);
        Task<List<Employee>> GetFirstThreeEmployees();
        Task<bool> UpdateEmployeeBySp(Employee emp);
        Task<bool> DeleteEmployeeByIdBySp(int id);
        Task<List<Company>> GetCompanies();
        Task<bool> SendMail(string receiverEmailAddress, StringBuilder body = null);
        Task<bool> LoginUser(string email, string password);
        Task<Login> GetUserByEmail(string email);
        Task<bool> RegisterUser(Login user);
        Task<List<DatatableEmp>> GetDataTableEmps();
        Task<DatatableResponse> GetDataTableEmps(DatatableProperties dtProp);
        Task AddException(IExceptionHandlerPathFeature exceptionHandlerPathFeature);
        Task<bool> UpdateUserToken(long userId, Guid token);
        Task<bool> PasswordResetNotificationOnMail(string email, Guid token);
        Task<Login> GetUserByToken(Guid token);
        Task<bool> UpdateUserPassword(long userId, string salt, string password);
    }
}
