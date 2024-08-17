using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Service;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class ADOSPController : Controller
    {
        ISampleService sampleService;
        public ADOSPController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }
        public async Task<IActionResult> Index()
        {
            List<Employee> lst_employee = await this.sampleService.GetEmployeesBySp();
            return View(lst_employee);
        }
        [HttpGet]
        public async Task<IActionResult> AddUpdate(int id)
        {
            Employee emp = await this.sampleService.GetEmployeeByIdBySp(id);
            if (emp == null)
            {
                return View(new Employee());
            }
            else
            {
                return View(emp);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUpdate(Employee emp)
        {
            if (ModelState.IsValid)
            {
                if (emp.EmpId == 0) // Create
                {
                    bool create = await this.sampleService.CreateEmployeeBySp(emp);
                    if (create)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.errmsg = "Could not add record!!";
                        return View();
                    }
                }
                else // Update
                {
                    bool update = await this.sampleService.UpdateEmployeeBySp(emp);
                    if (update)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.errmsg = "Could not update record!!";
                        return View();
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                bool delete = await this.sampleService.DeleteEmployeeByIdBySp(id);
                if (delete)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errmsg = "Could not delete record!!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.errmsg = "Could not delete record!!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MultiRecordToSp()
        {
            List<Employee> empList = new List<Employee>();
            for (int i=1; i<=3; i++)
            {
                var emp = new Employee { EmpName = "", EmpPhone = "", EmpCity = "", EmpEmail = "", EmpState = "", EmpAddress = "", EmpCountry = "" };
                empList.Add(emp);
            }
            EmployeeVM model = new EmployeeVM {
                Employees = empList
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MultiRecordToSp(EmployeeVM model)
        {
            List<Employee> lst_employee = await this.sampleService.CreateMultiEmployeesBySp(model.Employees);
            string msg = "";
            if (lst_employee.Count > 0)
            {
                msg = "Added record!!";
                return Json(new { success = true, msg = msg, data = lst_employee });
            }
            else
            {
                msg = "Could not add record!!";
                return Json(new { success = false, msg = msg });
            }
            
        }

        public async Task<IActionResult> MultiRecordUpdateBySp()
        {
            List<Employee> empList = new List<Employee>();
            empList = await this.sampleService.GetFirstThreeEmployees();
            EmployeeVM model = new EmployeeVM
            {
                Employees = empList
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MultiRecordUpdateBySp(EmployeeVM model)
        {
            List<Employee> lst_employee = await this.sampleService.UpdateMultiEmployeesBySp(model.Employees);
            string msg = "";
            if (lst_employee.Count > 0)
            {
                msg = "Updated record!!";
                return Json(new { success = true, msg = msg, data = lst_employee });
            }
            else
            {
                msg = "Could not update record!!";
                return Json(new { success = false, msg = msg });
            }

        }
    }
}
