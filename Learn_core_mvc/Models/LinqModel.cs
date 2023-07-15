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

    public class LinqDepEmpAddrCoun
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
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
        public int AddressId { get; set; }

        public static List<LinqEmployee> GetAllEmployees()
        {
            return new List<LinqEmployee>()
            {
                new LinqEmployee { ID = 1, Name = "Mark", DepartmentID = 1, AddressId=1 },
                new LinqEmployee { ID = 2, Name = "Steve", DepartmentID = 2, AddressId=2 },
                new LinqEmployee { ID = 3, Name = "Ben", DepartmentID = 1, AddressId=3 },
                new LinqEmployee { ID = 4, Name = "Philip", DepartmentID = 1,AddressId=4 },
                new LinqEmployee { ID = 5, Name = "Mary", DepartmentID = 2, AddressId=5 },
                new LinqEmployee { ID = 6, Name = "Valarie", DepartmentID = 2, AddressId=6 },
                new LinqEmployee { ID = 7, Name = "John", DepartmentID = 1, AddressId=7 },
                new LinqEmployee { ID = 8, Name = "Pam", DepartmentID = 1, AddressId=8 },
                new LinqEmployee { ID = 9, Name = "Stacey", DepartmentID = 2, AddressId=9 },
                new LinqEmployee { ID = 10, Name = "Andy", DepartmentID = 1, AddressId=10 }
            };
        }
    }

    public class LinqAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }

        public static List<LinqAddress> GetAllAddress()
        {
            return new List<LinqAddress>()
            {
                new LinqAddress { Id = 1, Address = "Address 1", CountryId = 1 },
                new LinqAddress { Id = 2, Address = "Address 2", CountryId = 2 },
                new LinqAddress { Id = 3, Address = "Address 3", CountryId = 3 },
                new LinqAddress { Id = 4, Address = "Address 4", CountryId = 2 },
                new LinqAddress { Id = 5, Address = "Address 5", CountryId = 4 },
                new LinqAddress { Id = 6, Address = "Address 6", CountryId = 5 },
                new LinqAddress { Id = 7, Address = "Address 7", CountryId = 1 },
                new LinqAddress { Id = 8, Address = "Address 8", CountryId = 3 },
                new LinqAddress { Id = 9, Address = "Address 9", CountryId = 2 },
                new LinqAddress { Id = 10, Address = "Address 10", CountryId = 1}
            };
        }
    }

    public class LinqCountry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<LinqCountry> GetAllCountries()
        {
            return new List<LinqCountry>()
            {
                new LinqCountry { Id = 1, Name = "India"},
                new LinqCountry { Id = 2, Name = "China"},
                new LinqCountry { Id = 3, Name = "America"},
                new LinqCountry { Id = 4, Name = "Australia"},
                new LinqCountry { Id = 5, Name = "Jermany"}
            };
        }
    }
}
