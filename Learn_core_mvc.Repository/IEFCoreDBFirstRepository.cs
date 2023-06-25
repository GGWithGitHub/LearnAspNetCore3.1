﻿using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public interface IEFCoreDBFirstRepository
    {
        Task<List<TblEmployee>> GetEmployees();
        Task<TblEmployee> GetEmployeeById(int empId);
        Task<bool> DeleteEmployee(int empId);
        Task<bool> CreateEmployee(TblEmployee emp);
        Task<bool> UpdateEmployee(TblEmployee emp);
    }
}