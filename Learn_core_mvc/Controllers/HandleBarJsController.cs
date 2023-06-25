using Learn_core_mvc.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class HandleBarJsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<HandleBarJs> GetEmpList()
        {
            List<HandleBarJs> listEmp = new List<HandleBarJs>() {
                new HandleBarJs()
                {
                    EmpId=1, EmpName="Aman", EmpNumber=9854785698, EmpEmail="aman@gmail.com", EmpCompany="TCS"
                },
                new HandleBarJs()
                {
                    EmpId=2,EmpName="Bman", EmpNumber=8854785698, EmpEmail="bman@gmail.com", EmpCompany="Vipro"
                },
                new HandleBarJs()
                {
                    EmpId=3,EmpName="Cman", EmpNumber=7854785698, EmpEmail="cman@gmail.com", EmpCompany="Accenture"
                },new HandleBarJs()
                {
                    EmpId=4,EmpName="Dman", EmpNumber=6854785698, EmpEmail="dman@gmail.com", EmpCompany="IBM"
                },
                new HandleBarJs()
                {
                    EmpId=5,EmpName="Eman", EmpNumber=5854785698, EmpEmail="eman@gmail.com", EmpCompany="Mahindra"
                }
            };
            return listEmp;
        }

        public IActionResult GetEmployeeDetails()
        {
            return Json(new { Data = GetEmpList() });
        }

        public Dictionary<long, bool> GetEmployeesBenfitDetails()
        {
            Dictionary<long, bool> dicBenefit = new Dictionary<long, bool>();
            dicBenefit.Add(1, true);
            dicBenefit.Add(2, false);
            dicBenefit.Add(3, false);
            dicBenefit.Add(4, false);
            dicBenefit.Add(5, true);
            return dicBenefit;
        }

        public IActionResult GetBenefit(long empId)
        {
            Dictionary<long, bool> dicBenefit = GetEmployeesBenfitDetails();

            bool willGetBenefit = dicBenefit.FirstOrDefault(x => x.Key == empId).Value;

            List<HandleBarJs> listEmp = GetEmpList();
            HandleBarJs emp = listEmp.Where(x => x.EmpId == empId).FirstOrDefault();
            emp.EmpEligible = willGetBenefit;

            return Json(new { Data = emp });
        }

        public IActionResult SendMoney(long empId)
        {
            Dictionary<long, bool> dicBenefit = GetEmployeesBenfitDetails();

            bool willGetBenefit = dicBenefit.FirstOrDefault(x => x.Key == empId).Value;

            return Json(new { Data = willGetBenefit });
        }
    }
}
