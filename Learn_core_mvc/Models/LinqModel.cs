using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class LinqTestEmployee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class LinqBook
    {
        public int BookID { get; set; }
        public string BookNm { get; set; }
    }

    public class LinqOrder
    {
        public int OrderID { get; set; }
        public int BookID { get; set; }
        public string PaymentMode { get; set; }
    }

    public class LinqBookOrder
    {
        public int BookID { get; set; }
        public string BookNm { get; set; }
        public int OrderID { get; set; }
        public string PaymentMode { get; set; }
    }

    public class LinqDepartmentEmployee
    {
        public LinqDepartment Department { get; set; }
        public List<LinqEmployee> Employees { get; set; }
    }

    public class LinqDepartment
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public static List<LinqDepartment> GetAllDepartments()
        {
            return new List<LinqDepartment>()
            {
                new LinqDepartment { ID = 1, Name = "IT"},
                new LinqDepartment { ID = 2, Name = "HR"},
                new LinqDepartment { ID = 3, Name = "Payroll"},
            };
        }
    }

    public class LinqEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }

        public static List<LinqEmployee> GetAllEmployees()
        {
            return new List<LinqEmployee>()
            {
                new LinqEmployee { ID = 1, Name = "Mark", DepartmentID = 1 },
                new LinqEmployee { ID = 2, Name = "Steve", DepartmentID = 2 },
                new LinqEmployee { ID = 3, Name = "Ben", DepartmentID = 1 },
                new LinqEmployee { ID = 4, Name = "Philip", DepartmentID = 1 },
                new LinqEmployee { ID = 5, Name = "Mary", DepartmentID = 2 },
                new LinqEmployee { ID = 6, Name = "Valarie", DepartmentID = 2 },
                new LinqEmployee { ID = 7, Name = "John", DepartmentID = 1 },
                new LinqEmployee { ID = 8, Name = "Pam", DepartmentID = 1 },
                new LinqEmployee { ID = 9, Name = "Stacey", DepartmentID = 2 },
                new LinqEmployee { ID = 10, Name = "Andy", DepartmentID = 1}
            };
        }
    }
}
