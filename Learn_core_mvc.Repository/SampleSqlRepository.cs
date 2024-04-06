using Learn_core_mvc.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Repository
{
    //public class SampleSqlRepository<T> : SqlRepository<T>, ISampleSqlRepository<T> where T : new()
    public class SampleSqlRepository : SqlRepository, ISampleSqlRepository
    {
        private const string RETRIEVE_ALL_QUERY = @"SELECT * FROM tbl_employee";
        private const string RETRIEVE_QUERY = @"SELECT * FROM tbl_employee WHERE emp_id=@emp_id;";
        private const string USER_LOGIN_QUERY = @"SELECT * FROM tbl_user WHERE user_email=@user_email;";
        private const string UPDATE_QUERY = @"UPDATE tbl_employee
                                                SET
                                                emp_name = @emp_name,
                                                emp_email = @emp_email,
                                                emp_phone = @emp_phone,
                                                emp_address = @emp_address,
                                                emp_city = @emp_city,
                                                emp_state = @emp_state,
                                                emp_country = @emp_country
                                                WHERE emp_id = @emp_id;";

        private const string INSERT_QUERY = @"INSERT INTO tbl_employee
                                                (
                                                    emp_name,
                                                    emp_email,
                                                    emp_phone,
                                                    emp_address,
                                                    emp_city,
                                                    emp_state,
                                                    emp_country
                                                )
                                                VALUES
                                                (
                                                    @emp_name,
                                                    @emp_email,
                                                    @emp_phone,
                                                    @emp_address,
                                                    @emp_city,
                                                    @emp_state,
                                                    @emp_country
                                                );";

        private const string USER_REGISTER_QUERY = @"INSERT INTO tbl_user
                                                (
                                                    user_email,
                                                    user_password,
                                                    user_salt,
                                                    user_role_id
                                                )
                                                VALUES
                                                (
                                                    @user_email,
                                                    @user_password,
                                                    @user_salt,
                                                    @user_role_id
                                                );";

        private const string DELETE_QUERY = @"DELETE FROM tbl_employee WHERE emp_id = @emp_id;";

        private const string RETRIEVE_COMPANIES = @"SELECT * FROM tbl_company";

        private const string RETRIEVE_DT_EMPS_All = @"select * from tbl_datatable_emp;";

        private const string RETRIEVE_DT_EMPS_All_COUNT = @"select count(*) as allcount from tbl_datatable_emp;";

        private const string RETRIEVE_DT_FILTER_EMPS_All_COUNT = @"SELECT COUNT(*) as allcount from tbl_datatable_emp WHERE emp_name LIKE @searchValue OR emp_email LIKE @searchValue OR emp_city LIKE @searchValue";

        private const string INSERT_EXCEPTION_LOGGER_QUERY = @"INSERT INTO tbl_Exception_logger
                                                            (
                                                                Exception_message,
                                                                Exception_stack_trace,
                                                                Log_time
                                                            )
                                                            VALUES
                                                            (
                                                                @Exception_message,
                                                                @Exception_stack_trace,
                                                                @Log_time
                                                            );";

        private const string RETRIEVE_USER_BY_EMAIL_QUERY = @"SELECT * FROM tbl_user WHERE user_email=@user_email";

        private const string USER_UPDATE_TOKEN_QUERY = @"UPDATE tbl_user SET user_token=@user_token WHERE user_id=@user_id";

        private const string RETRIEVE_USER_BY_TOKEN_QUERY = @"SELECT * FROM tbl_user WHERE user_token=@user_token";

        private const string USER_UPDATE_PASSWORD_QUERY = @"UPDATE tbl_user SET user_password=@user_password WHERE user_id=@user_id";
        public SampleSqlRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<Employee> GetEmployee(int id)
        {
            Employee employee = null;
            using (var employeeData = await ExecuteQuery(RETRIEVE_QUERY, new Dictionary<string, object>() { { "@emp_id", id } }))
            {
                employee = GetToList(
                                employeeData,
                                x => new Employee()
                                {
                                    EmpId = x.Field<int>("emp_id"),
                                    EmpName = x.Field<string>("emp_name"),
                                    EmpEmail = x.Field<string>("emp_email"),
                                    EmpPhone = x.Field<string>("emp_phone"),
                                    EmpAddress = x.Field<string>("emp_address"),
                                    EmpCity = x.Field<string>("emp_city"),
                                    EmpState = x.Field<string>("emp_state"),
                                    EmpCountry = x.Field<string>("emp_country")
                                }
                            ).FirstOrDefault();
            }

            return employee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = new List<Employee>();
            using (var employeeData = await ExecuteQuery(RETRIEVE_ALL_QUERY, new Dictionary<string, object>() { }))
            {
                employees = GetToList(
                                employeeData,
                                x => new Employee()
                                {
                                    EmpId = x.Field<int>("emp_id"),
                                    EmpName = x.Field<string>("emp_name"),
                                    EmpEmail = x.Field<string>("emp_email"),
                                    EmpPhone = x.Field<string>("emp_phone"),
                                    EmpAddress = x.Field<string>("emp_address"),
                                    EmpCity = x.Field<string>("emp_city"),
                                    EmpState = x.Field<string>("emp_state"),
                                    EmpCountry = x.Field<string>("emp_country")
                                }
                            );
            }

            return employees;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            bool isSuccessful = false;

            var parameters = new Dictionary<string, object>()
            {
                { "@emp_id", id },
            };

            isSuccessful = await ExecuteNonQuery(DELETE_QUERY, parameters);

            return isSuccessful;
        }

        public async Task<bool> CreateEmployee(Employee emp)
        {
            bool isSuccessful = false;

            var parameters = new Dictionary<string, object>()
            {
                { "@emp_name", emp.EmpName },
                { "@emp_email", emp.EmpEmail },
                { "@emp_phone", emp.EmpPhone },
                { "@emp_address", emp.EmpAddress },
                { "@emp_city", emp.EmpCity },
                { "@emp_state", emp.EmpState },
                { "@emp_country", emp.EmpCountry }
            };

            isSuccessful = await ExecuteNonQuery(INSERT_QUERY, parameters);

            return isSuccessful;
        }
        public async Task<bool> UpdateEmployee(Employee emp)
        {
            bool isSuccessful = false;

            var parameters = new Dictionary<string, object>()
            {
                { "@emp_id", emp.EmpId },
                { "@emp_name", emp.EmpName },
                { "@emp_email", emp.EmpEmail },
                { "@emp_phone", emp.EmpPhone },
                { "@emp_address", emp.EmpAddress },
                { "@emp_city", emp.EmpCity },
                { "@emp_state", emp.EmpState },
                { "@emp_country", emp.EmpCountry }
            };

            isSuccessful = await ExecuteNonQuery(UPDATE_QUERY, parameters);

            return isSuccessful;
        }

        public async Task<Employee> GetEmployeeByIdBySp(int id)
        {
            Employee employee = null;
            using (var employeeData = await ExecuteCommand("spGetEmployeeById", new Dictionary<string, object>() { { "@EmpId", id } }))
            {
                employee = GetToList(
                                employeeData,
                                x => new Employee()
                                {
                                    EmpId = x.Field<int>("emp_id"),
                                    EmpName = x.Field<string>("emp_name"),
                                    EmpEmail = x.Field<string>("emp_email"),
                                    EmpPhone = x.Field<string>("emp_phone"),
                                    EmpAddress = x.Field<string>("emp_address"),
                                    EmpCity = x.Field<string>("emp_city"),
                                    EmpState = x.Field<string>("emp_state"),
                                    EmpCountry = x.Field<string>("emp_country")
                                }
                            ).FirstOrDefault();
            }

            return employee;
        }

        public async Task<List<Employee>> GetEmployeesBySp()
        {
            var employees = new List<Employee>();
            using (var employeeData = await ExecuteCommand("spGetEmployees", new Dictionary<string, object>() { }))
            {
                employees = GetToList(
                                employeeData,
                                x => new Employee()
                                {
                                    EmpId = x.Field<int>("emp_id"),
                                    EmpName = x.Field<string>("emp_name"),
                                    EmpEmail = x.Field<string>("emp_email"),
                                    EmpPhone = x.Field<string>("emp_phone"),
                                    EmpAddress = x.Field<string>("emp_address"),
                                    EmpCity = x.Field<string>("emp_city"),
                                    EmpState = x.Field<string>("emp_state"),
                                    EmpCountry = x.Field<string>("emp_country")
                                }
                            );
            }

            return employees;
        }
        public async Task<bool> CreateEmployeeBySp(Employee emp)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@emp_name", emp.EmpName },
                { "@emp_email", emp.EmpEmail },
                { "@emp_phone", emp.EmpPhone },
                { "@emp_address", emp.EmpAddress },
                { "@emp_city", emp.EmpCity },
                { "@emp_state", emp.EmpState },
                { "@emp_country", emp.EmpCountry }
            };

            var isSuccessful = await ExecuteNonCommand("spCreateEmployee", parameters);

            return isSuccessful;
        }

        public async Task<List<Employee>> CreateMultiEmployeesBySp(List<Employee> emps)
        {
            MyDataTable dataTable = new MyDataTable();
            dataTable.DataTableParam = "@empDataTableParam";
            dataTable.DataTableTypeName = "dbo.EmpDataTableTypeName";

            DataTable dt = new DataTable();

            // Add columns to the DataTable
            dt.Columns.Add("emp_name", typeof(string));
            dt.Columns.Add("emp_email", typeof(string));
            dt.Columns.Add("emp_phone", typeof(string));
            dt.Columns.Add("emp_address", typeof(string));
            dt.Columns.Add("emp_city", typeof(string));
            dt.Columns.Add("emp_state", typeof(string));
            dt.Columns.Add("emp_country", typeof(string));

            // Add rows to the DataTable
            foreach (var employee in emps)
            {
                dt.Rows.Add(employee.EmpName, employee.EmpEmail, employee.EmpPhone, employee.EmpAddress, employee.EmpCity, employee.EmpState, employee.EmpCountry);
            }

            dataTable.DataTable = dt;
            
            var employees = new List<Employee>();
            using (var employeeData = await ExecuteTableType("dbo.spAddMultiEmployees", dataTable))
            {
                employees = GetToList(
                                employeeData,
                                x => new Employee()
                                {
                                    EmpId = x.Field<int>("emp_id"),
                                    EmpName = x.Field<string>("emp_name"),
                                    EmpEmail = x.Field<string>("emp_email"),
                                    EmpPhone = x.Field<string>("emp_phone"),
                                    EmpAddress = x.Field<string>("emp_address"),
                                    EmpCity = x.Field<string>("emp_city"),
                                    EmpState = x.Field<string>("emp_state"),
                                    EmpCountry = x.Field<string>("emp_country")
                                }
                            );
            }

            return employees;
        }
        public async Task<bool> UpdateEmployeeBySp(Employee emp)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@emp_id", emp.EmpId },
                { "@emp_name", emp.EmpName },
                { "@emp_email", emp.EmpEmail },
                { "@emp_phone", emp.EmpPhone },
                { "@emp_address", emp.EmpAddress },
                { "@emp_city", emp.EmpCity },
                { "@emp_state", emp.EmpState },
                { "@emp_country", emp.EmpCountry }
            };

            var isSuccessful = await ExecuteNonCommand("spUpdateEmployeeById", parameters);

            return isSuccessful;
        }
        public async Task<bool> DeleteEmployeeByIdBySp(int id)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@emp_id", id },
            };

            var isSuccessful = await ExecuteNonCommand("spDeleteEmployeeById", parameters);

            return isSuccessful;
        }
        public async Task<List<Company>> GetCompanies()
        {
            var companies = new List<Company>();
            using (var companyData = await ExecuteQuery(RETRIEVE_COMPANIES, new Dictionary<string, object>() { }))
            {
                companies = GetToList(
                                companyData,
                                x => new Company()
                                {
                                    CmpId = x.Field<int>("cmp_id"),
                                    CmpName = x.Field<string>("cmp_name"),
                                    CmpAddress = x.Field<string>("cmp_address"),
                                    CmpRating = x.Field<int>("cmp_rating")
                                }
                            );
            }

            return companies;
        }

        public async Task<bool> RegisterUser(Login user)
        {
            bool isSuccessful = false;
            var parameters = new Dictionary<string, object>()
            {
                { "@user_email", user.UserEmail },
                { "@user_password", user.UserPassword },
                { "@user_salt", user.UserSalt },
                { "@user_role_id", user.UserRoleId }
            };

            isSuccessful = await ExecuteNonQuery(USER_REGISTER_QUERY, parameters);

            return isSuccessful;
        }

        public async Task<Login> GetUserByEmail(string email)
        {
            Login user = null;
            using (var userData = await ExecuteQuery(RETRIEVE_USER_BY_EMAIL_QUERY, new Dictionary<string, object>() { { "@user_email", email } }))
            {
                user = GetToList(
                                userData,
                                x => new Login()
                                {
                                    UserId = x.Field<Int64>("user_id"),
                                    UserEmail = x.Field<string>("user_email"),
                                    UserPassword = x.Field<string>("user_password"),
                                    UserRoleId = x.Field<int>("user_role_id")
                                }
                            ).FirstOrDefault();
            }

            return user;
        }

        public async Task<Login> GetUserByToken(Guid token)
        {
            Login user = null;
            using (var userData = await ExecuteQuery(RETRIEVE_USER_BY_TOKEN_QUERY, new Dictionary<string, object>() { { "@user_token", token } }))
            {
                user = GetToList(
                                userData,
                                x => new Login()
                                {
                                    UserId = x.Field<Int64>("user_id"),
                                    Token = x.Field<Guid>("user_token"),
                                    UserPassword = x.Field<string>("user_password"),
                                    UserSalt = x.Field<string>("user_salt")
                                }
                            ).FirstOrDefault();
            }

            return user;
        }

        public async Task<bool> UpdateUserPassword(long userId, string password)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@user_id", userId },
                { "@user_password", password }
            };

            var isSuccessful = await ExecuteNonQuery(USER_UPDATE_PASSWORD_QUERY, parameters);

            return isSuccessful;
        }

        public async Task<bool> UpdateUserToken(long userId, Guid token)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@user_id", userId },
                { "@user_token", token }
            };

            var isSuccessful = await ExecuteNonQuery(USER_UPDATE_TOKEN_QUERY, parameters);

            return isSuccessful;
        }

        public async Task<Login> LoginUser(string email)
        {
            Login user = null;
            using (var userData = await ExecuteQuery(USER_LOGIN_QUERY, new Dictionary<string, object>() { { "@user_email", email } }))
            {
                var data = GetToList(
                                userData,
                                x => new Login()
                                {
                                    UserId = x.Field<long>("user_id"),
                                    UserEmail = x.Field<string>("user_email"),
                                    UserPassword = x.Field<string>("user_password"),
                                    UserSalt = x.Field<string>("user_salt"),
                                    UserRoleId = x.Field<int>("user_role_id")
                                }
                            ).FirstOrDefault();
                if (data != null)
                {
                    user = data;
                }
            }

            return user;
        }

        public async Task<List<DatatableEmp>> GetDTEmps()
        {
            List<DatatableEmp> dtEmps = null;

            try
            {
                using (var dtEmpData = await ExecuteQuery(RETRIEVE_DT_EMPS_All))
                {
                    dtEmps = GetToList(
                                    dtEmpData,
                                    x => new DatatableEmp()
                                    {
                                        EmpId = x.Field<int>("emp_id"),
                                        EmpName = x.Field<string>("emp_name"),
                                        EmpEmail = x.Field<string>("emp_email"),
                                        EmpGender = x.Field<string>("emp_gender"),
                                        EmpSalary = x.Field<decimal>("emp_salary"),
                                        EmpCity = x.Field<string>("emp_city"),
                                    }
                                );
                }
            }
            catch (Exception ex)
            {

            }

            return dtEmps;
        }
        
        public async Task<List<DatatableEmp>> GetDTEmpsLimit(DatatableProperties dtProp)
        {
            List<DatatableEmp> dtEmps = null;

            string limitQuery = $"SELECT * FROM tbl_datatable_emp WHERE (emp_name LIKE '%{dtProp.SearchValue}%' OR emp_email LIKE '%{dtProp.SearchValue}%' OR emp_city LIKE '%{dtProp.SearchValue}%') ORDER BY {dtProp.ColumnName} {dtProp.ColumnSortOrder} OFFSET {dtProp.Row} ROWS FETCH NEXT {dtProp.RowPerPage} ROWS ONLY";

            try
            {
                using (var dtEmpData = await ExecuteQuery(limitQuery))
                {
                    dtEmps = GetToList(
                                    dtEmpData,
                                    x => new DatatableEmp()
                                    {
                                        EmpId = x.Field<int>("emp_id"),
                                        EmpName = x.Field<string>("emp_name"),
                                        EmpEmail = x.Field<string>("emp_email"),
                                        EmpGender = x.Field<string>("emp_gender"),
                                        EmpSalary = x.Field<decimal>("emp_salary"),
                                        EmpCity = x.Field<string>("emp_city"),
                                    }
                                );
                }
            }
            catch (Exception ex)
            {

            }
            
            return dtEmps;
        }

        public async Task<int> GetDTTotalEmpsCount()
        {
            int totalRecords = 0;
            using (var dtEmpData = await ExecuteQuery(RETRIEVE_DT_EMPS_All_COUNT))
            { 
                var dtEmp = GetToList(
                                dtEmpData,
                                x => new DatatableEmp()
                                {
                                    EmpAllCount = x.Field<int>("allcount")
                                }
                            ).FirstOrDefault();
                totalRecords = dtEmp.EmpAllCount;
            }
            return totalRecords;
        }

        public async Task<int> GetDTTotalFilterEmpsCount(string searchValue)
        {
            int totalRecords = 0;
            var parameters = new Dictionary<string, object>() { { "@searchValue", string.Format("%{0}%", searchValue) } };
            try
            {
                using (var dtEmpData = await ExecuteQuery(RETRIEVE_DT_FILTER_EMPS_All_COUNT, parameters))
                {
                    var dtEmp = GetToList(
                                    dtEmpData,
                                    x => new DatatableEmp()
                                    {
                                        EmpAllCount = x.Field<int>("allcount")
                                    }
                                ).FirstOrDefault();
                    totalRecords = dtEmp.EmpAllCount;
                }
            }
            catch (Exception ex)
            {

            }
            return totalRecords;
        }
        public async Task InsertException(ExceptionLogger exceptionLogger)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "@Exception_message", exceptionLogger.ExceptionMessage },
                { "@Exception_stack_trace", exceptionLogger.ExceptionStackTrace },
                { "@Log_time", exceptionLogger.LogTime }
            };

            var result = await ExecuteNonQuery(INSERT_EXCEPTION_LOGGER_QUERY, parameters);
        }
    }
}
