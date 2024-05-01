using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class ExcelDataModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal? Salary { get; set; }

        public List<ExcelDataModel> GetExcelDatas()
        {
            List<ExcelDataModel> excelDatas = new List<ExcelDataModel> {
                new ExcelDataModel{ 
                    Name = "Amit",
                    Email = "amit@test.com",
                    Phone = "9087898789",
                    City = "Indore",
                    State = "MP",
                    Salary = 10000
                },
                new ExcelDataModel{
                    Name = "Bmit",
                    Email = "bmit@test.com",
                    Phone = "8087898789",
                    City = "Dewas",
                    State = "MP",
                    Salary = 20000
                },
                new ExcelDataModel{
                    Name = "Cmit",
                    Email = "cmit@test.com",
                    Phone = "7087898789",
                    City = "Ujjain",
                    State = "MP",
                    Salary = 12000
                },
                new ExcelDataModel{
                    Name = "Dmit",
                    Email = "dmit@test.com",
                    Phone = "6087898789",
                    City = "Rau",
                    State = "MP",
                    Salary = 15000
                },
                new ExcelDataModel{
                    Name = "Emit",
                    Email = "emit@test.com",
                    Phone = "5087898789",
                    City = "Bhopal",
                    State = "MP",
                    Salary = 18000
                },
                new ExcelDataModel{
                    Name = "Fmit",
                    Email = "fmit@test.com",
                    Phone = "4087898789",
                    City = "Indore",
                    State = "MP",
                    Salary = 19000
                },
                new ExcelDataModel{
                    Name = "Gmit",
                    Email = "gmit@test.com",
                    Phone = "9987898789",
                    City = "Mahu",
                    State = "MP",
                    Salary = 22000
                },
                new ExcelDataModel{
                    Name = "Hmit",
                    Email = "hmit@test.com",
                    Phone = "8887898789",
                    City = "Indore",
                    State = "MP",
                    Salary = 16000
                },
                new ExcelDataModel{
                    Name = "Imit",
                    Email = "imit@test.com",
                    Phone = "7787898789",
                    City = "Indore",
                    State = "MP",
                    Salary = 11000
                },
                new ExcelDataModel{
                    Name = "Jmit",
                    Email = "jmit@test.com",
                    Phone = "6687898789",
                    City = "Indore",
                    State = "MP",
                    Salary = 17000
                }
            };
            return excelDatas;
        }
    }

    public class ChatGptReqModel
    {
        public string ReqContent { get; set; }
    }
}
