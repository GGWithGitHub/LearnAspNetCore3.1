using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Service;
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
    }
}
