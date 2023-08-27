using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class EFCoreDbFirstController : Controller
    {
        private readonly IEFCoreDBFirstService _eFCoreDBFirstService;
        public EFCoreDbFirstController(IEFCoreDBFirstService eFCoreDBFirstService)
        {
            _eFCoreDBFirstService = eFCoreDBFirstService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployees()
        {
            List<EmployeeForEFCoreDbFirst> lst_employee = await _eFCoreDBFirstService.GetEmployees();
            return View(lst_employee);
        }

        public async Task<IActionResult> GetEmployeeDetail(int empId)
        {
            EmployeeForEFCoreDbFirst emp = await _eFCoreDBFirstService.GetEmployeeById(empId);
            return View(emp);
        }

        public async Task<IActionResult> CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployee(EmployeeForEFCoreDbFirst emp)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _eFCoreDBFirstService.CreateEmployee(emp);
                if (isCreated)
                {
                    return RedirectToAction("GetEmployees");
                }
            }

            return View();
        }

        public async Task<IActionResult> UpdateEmployee(int empId)
        {
            EmployeeForEFCoreDbFirst emp = await _eFCoreDBFirstService.GetEmployeeById(empId);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(EmployeeForEFCoreDbFirst emp)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = await _eFCoreDBFirstService.UpdateEmployee(emp);
                if (isUpdated)
                {
                    return RedirectToAction("GetEmployees");
                }
            }
            return View(emp);
        }

        public async Task<IActionResult> DeleteEmployee(int empId)
        {
            EmployeeForEFCoreDbFirst emp = await _eFCoreDBFirstService.GetEmployeeById(empId);
            return View(emp);
        }

        [HttpPost, ActionName("DeleteEmployee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(EmployeeForEFCoreDbFirst emp)
        {
            //if (ModelState.IsValid)
            //{
                bool isDeleted = await _eFCoreDBFirstService.DeleteEmployee(emp.EmpId);
                if (isDeleted)
                {
                    return RedirectToAction("GetEmployees");
                }
            //}
            return View(emp);
        }
    }
}
