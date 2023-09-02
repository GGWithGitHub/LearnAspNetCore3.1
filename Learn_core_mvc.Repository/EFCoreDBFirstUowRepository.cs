using Learn_core_mvc.Repository.EFDBFirstRepo;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public class EFCoreDBFirstUowRepository: IEFCoreDBFirstUowRepository
    {
        private readonly MyDBDbContext _dbContext;
        public EFCoreDBFirstUowRepository(MyDBDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TblEmployee>> GetEmployees()
        {
            var employees = new List<TblEmployee>();
            employees = await _dbContext.TblEmployee.ToListAsync();
            return employees;
        }

        public async Task<TblEmployee> GetEmployeeById(int empId)
        {
            var employee = new TblEmployee();
            employee = await _dbContext.TblEmployee.Where(x => x.EmpId == empId).FirstOrDefaultAsync();
            return employee;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            bool isSuccessful = false;
            var emp = await _dbContext.TblEmployee.Where(x => x.EmpId == empId).FirstOrDefaultAsync();
            if (emp != null)
            {
                _dbContext.TblEmployee.Remove(emp);
                isSuccessful = true;
            }
            return isSuccessful;
        }

        public async Task<bool> CreateEmployee(TblEmployee emp)
        {
            bool isSuccessful = false;
            await _dbContext.TblEmployee.AddAsync(emp);
            isSuccessful = true;
            return isSuccessful;
        }

        public async Task<bool> UpdateEmployee(TblEmployee emp)
        {
            bool isSuccessful = false;
            var employee = await _dbContext.TblEmployee.Where(x => x.EmpId == emp.EmpId).FirstOrDefaultAsync();
            if (employee != null)
            {
                employee.EmpAddress = emp.EmpAddress;
                employee.EmpCity = emp.EmpCity;
                employee.EmpCountry = emp.EmpCountry;
                employee.EmpEmail = emp.EmpEmail;
                employee.EmpName = emp.EmpName;
                employee.EmpPhone = emp.EmpPhone;
                employee.EmpState = emp.EmpState;

                isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
