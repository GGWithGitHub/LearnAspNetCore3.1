using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    public class EFCoreDBFirstRepository : IEFCoreDBFirstRepository
    {
        public EFCoreDBFirstRepository()
        {

        }

        public async Task<List<TblEmployee>> GetEmployees()
        {
            var employees = new List<TblEmployee>();
            using (var dbContext = new MyDBDbContext())
            {
                employees = await dbContext.TblEmployee.ToListAsync();
            }
            return employees;
        }

        public async Task<TblEmployee> GetEmployeeById(int empId)
        {
            var employee = new TblEmployee();
            using (var dbContext = new MyDBDbContext())
            {
                employee = await dbContext.TblEmployee.Where(x => x.EmpId == empId).FirstOrDefaultAsync();
            }
            return employee;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            bool isSuccessful = false;
            using (var dbContext = new MyDBDbContext())
            {
                var emp = await dbContext.TblEmployee.Where(x => x.EmpId == empId).FirstOrDefaultAsync();
                if (emp != null)
                {
                    dbContext.TblEmployee.Remove(emp);
                    await dbContext.SaveChangesAsync();
                    isSuccessful = true;
                }
            }
            return isSuccessful;
        }

        public async Task<bool> CreateEmployee(TblEmployee emp)
        {
            bool isSuccessful = false;
            using (var dbContext = new MyDBDbContext())
            {
                await dbContext.TblEmployee.AddAsync(emp);
                await dbContext.SaveChangesAsync();
                isSuccessful = true;
            }
            return isSuccessful;
        }

        public async Task<bool> UpdateEmployee(TblEmployee emp)
        {
            bool isSuccessful = false;
            using (var dbContext = new MyDBDbContext())
            {
                TblEmployee employee = new TblEmployee();
                employee.EmpAddress = emp.EmpAddress;
                employee.EmpCity = emp.EmpCity;
                employee.EmpCountry = emp.EmpCountry;
                employee.EmpEmail = emp.EmpEmail;
                employee.EmpName = emp.EmpName;
                employee.EmpPhone = emp.EmpPhone;
                employee.EmpState = emp.EmpState;

                dbContext.TblEmployee.Update(employee);
                //dbContext.Entry(emp).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
