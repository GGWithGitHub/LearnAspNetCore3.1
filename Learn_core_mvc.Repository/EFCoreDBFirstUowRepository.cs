using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo.SPModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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

        public async Task<List<EmpSpDbFirstRepoUowModel>> GetEmployeesSP()
        {
            var employees = await _dbContext.GetEmpsBySp
                    .FromSqlRaw("EXEC spGetEmployees")
                    .ToListAsync();
            var empModelList = new List<EmpSpDbFirstRepoUowModel>();
            if (employees.Count > 0)
            {
                foreach (var empItem in employees)
                {
                    var empModel = new EmpSpDbFirstRepoUowModel()
                    {
                        EmpId = empItem.emp_id,
                        EmpName = empItem.emp_name,
                        EmpAddress = empItem.emp_address,
                        EmpCity = empItem.emp_city,
                        EmpCountry = empItem.emp_country,
                        EmpEmail = empItem.emp_email,
                        EmpPhone = empItem.emp_phone,
                        EmpState = empItem.emp_state
                    };
                    empModelList.Add(empModel);
                }
            }
            return empModelList;
        }

        public async Task<EmpSpDbFirstRepoUowModel> GetEmployeeByIdSP(int empId)
        {
            var param1 = new SqlParameter("@EmpId", empId);
            var employee = _dbContext.GetEmpsBySp
                        .FromSqlRaw("EXEC spGetEmployeeById @EmpId", param1)
                        .AsEnumerable()
                        .FirstOrDefault();
            var empModel = new EmpSpDbFirstRepoUowModel();
            if (employee != null)
            {
                empModel.EmpId = employee.emp_id;
                empModel.EmpName = employee.emp_name;
                empModel.EmpAddress = employee.emp_address;
                empModel.EmpCity = employee.emp_city;
                empModel.EmpCountry = employee.emp_country;
                empModel.EmpEmail = employee.emp_email;
                empModel.EmpPhone = employee.emp_phone;
                empModel.EmpState = employee.emp_state;
            }
            return empModel;
        }

        public async Task<bool> CreateEmployeeSp(EmpSpDbFirstRepoUowModel emp)
        {
            bool isSuccessful = false;
            var param1 = new SqlParameter("@emp_name", emp.EmpName);
            var param2 = new SqlParameter("@emp_email", emp.EmpEmail);
            var param3 = new SqlParameter("@emp_phone", emp.EmpPhone);
            var param4 = new SqlParameter("@emp_address", emp.EmpAddress);
            var param5 = new SqlParameter("@emp_city", emp.EmpCity);
            var param6 = new SqlParameter("@emp_state", emp.EmpState);
            var param7 = new SqlParameter("@emp_country", emp.EmpCountry);
            int numAffectRow = _dbContext.Database
            .ExecuteSqlRaw("EXEC spCreateEmployee @emp_name,@emp_email,@emp_phone,@emp_address,@emp_city,@emp_state,@emp_country",
                    param1, param2, param3, param4, param5, param6, param7);
            if (numAffectRow == 1)
            {
                isSuccessful = true;
            }
            return isSuccessful;
        }

        public async Task<bool> UpdateEmployeeSp(EmpSpDbFirstRepoUowModel emp)
        {
            bool isSuccessful = false;
            var param1 = new SqlParameter("@emp_id", emp.EmpId);
            var param2 = new SqlParameter("@emp_name", emp.EmpName);
            var param3 = new SqlParameter("@emp_email", emp.EmpEmail);
            var param4 = new SqlParameter("@emp_phone", emp.EmpPhone);
            var param5 = new SqlParameter("@emp_address", emp.EmpAddress);
            var param6 = new SqlParameter("@emp_city", emp.EmpCity);
            var param7 = new SqlParameter("@emp_state", emp.EmpState);
            var param8 = new SqlParameter("@emp_country", emp.EmpCountry);
            int numAffectRow = _dbContext.Database
            .ExecuteSqlRaw("EXEC spUpdateEmployeeById @emp_id,@emp_name,@emp_email,@emp_phone,@emp_address,@emp_city,@emp_state,@emp_country",
                    param1, param2, param3, param4, param5, param6, param7,param8);
            if (numAffectRow == 1)
            {
                isSuccessful = true;
            }
            return isSuccessful;
        }

        public async Task<bool> DeleteEmployeeByIdSp(int empId)
        {
            bool isSuccessful = false;
            var param1 = new SqlParameter("@emp_id", empId);
            int numAffectRow = _dbContext.Database.ExecuteSqlRaw("EXEC spDeleteEmployeeById @emp_id", param1);
            if (numAffectRow == 1)
            {
                isSuccessful = true;
            }
            return isSuccessful;
        }
    }
}
