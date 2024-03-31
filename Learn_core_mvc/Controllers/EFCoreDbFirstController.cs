using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Models;
using Learn_core_mvc.Repository.EFDBFirstRepo;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using Learn_core_mvc.Service;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class EFCoreDbFirstController : Controller
    {
        private readonly IEFCoreDBFirstService _eFCoreDBFirstService;
        private readonly IEFCoreDBFirstRepoUowService _eFCoreDBFirstRepoUowService;
        public EFCoreDbFirstController(IEFCoreDBFirstService eFCoreDBFirstService,
            IEFCoreDBFirstRepoUowService eFCoreDBFirstRepoUowService)
        {
            _eFCoreDBFirstService = eFCoreDBFirstService;
            _eFCoreDBFirstRepoUowService = eFCoreDBFirstRepoUowService;
        }
        #region CRUD EF Core With DB First Approach And Repository Pattern
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
        #endregion

        #region CRUD Using EF Core With DB First Approach And Repository And Unit Of Work Pattern
        public async Task<IActionResult> CrudRepoUow()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployeesUow()
        {
            List<EmpDbFirstRepoUowModel> lst_employee = await _eFCoreDBFirstRepoUowService.GetEmployees();
            return View(lst_employee);
        }

        public async Task<IActionResult> GetEmployeeDetailUow(int empId)
        {
            EmpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeById(empId);
            return View(emp);
        }

        public async Task<IActionResult> CreateEmployeeUow()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployeeUow(EmpDbFirstRepoUowModel emp)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _eFCoreDBFirstRepoUowService.CreateEmployee(emp);
                if (isCreated)
                {
                    return RedirectToAction("GetEmployeesUow");
                }
            }

            return View();
        }

        public async Task<IActionResult> UpdateEmployeeUow(int empId)
        {
            EmpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeById(empId);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployeeUow(EmpDbFirstRepoUowModel emp)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = await _eFCoreDBFirstRepoUowService.UpdateEmployee(emp);
                if (isUpdated)
                {
                    return RedirectToAction("GetEmployeesUow");
                }
            }
            return View(emp);
        }

        public async Task<IActionResult> DeleteEmployeeUow(int empId)
        {
            EmpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeById(empId);
            return View(emp);
        }

        [HttpPost, ActionName("DeleteEmployeeUow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUow(EmpDbFirstRepoUowModel emp)
        {
            //if (ModelState.IsValid)
            //{
            bool isDeleted = await _eFCoreDBFirstRepoUowService.DeleteEmployee(emp.EmpId);
            if (isDeleted)
            {
                return RedirectToAction("GetEmployeesUow");
            }
            //}
            return View(emp);
        }
        #endregion

        #region CRUD Using SP In EF Core With DB First Approach And Repository
        public async Task<IActionResult> CrudSpRepoUow()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployeesSpUow()
        {
            List<EmpSpDbFirstRepoUowModel> lst_employee = await _eFCoreDBFirstRepoUowService.GetEmployeesSP();
            return View(lst_employee);
        }

        public async Task<IActionResult> GetEmployeeDetailSpUow(int empId)
        {
            EmpSpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeSP(empId);
            return View(emp);
        }

        public async Task<IActionResult> CreateEmployeeSpUow()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployeeSpUow(EmpSpDbFirstRepoUowModel emp)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _eFCoreDBFirstRepoUowService.CreateEmployeeSP(emp);
                if (isCreated)
                {
                    return RedirectToAction("GetEmployeesSpUow");
                }
            }

            return View();
        }

        public async Task<IActionResult> UpdateEmployeeSpUow(int empId)
        {
            EmpSpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeSP(empId);
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployeeSpUow(EmpSpDbFirstRepoUowModel emp)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = await _eFCoreDBFirstRepoUowService.UpdateEmployeeSP(emp);
                if (isUpdated)
                {
                    return RedirectToAction("GetEmployeesSpUow");
                }
            }
            return View(emp);
        }

        public async Task<IActionResult> DeleteEmployeeSpUow(int empId)
        {
            EmpSpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeSP(empId);
            return View(emp);
        }

        [HttpPost, ActionName("DeleteEmployeeSpUow")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSpUow(EmpSpDbFirstRepoUowModel emp)
        {
            //if (ModelState.IsValid)
            //{
            bool isDeleted = await _eFCoreDBFirstRepoUowService.DeleteEmployeeSP(emp.EmpId);
            if (isDeleted)
            {
                return RedirectToAction("GetEmployeesSpUow");
            }
            //}
            return View(emp);
        }
        #endregion

        #region CRUD Using EF Core With DB First Approach And Repository And Unit Of Work Pattern Part 2
        public async Task<IActionResult> CrudRepoUow2()
        {
            return View();
        }

        public async Task<IActionResult> GetEmployeesUow2()
        {
            List<EmpDbFirstRepoUowModel> lst_employee = await _eFCoreDBFirstRepoUowService.GetEmployees2();
            return View("GetEmployeesUow2", lst_employee);
        }

        public async Task<IActionResult> GetEmployeeDetailUow2(int empId)
        {
            EmpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeById2(empId);
            return View("GetEmployeeDetailUow2",emp);
        }

        public async Task<IActionResult> CreateEmployeeUow2()
        {
            return View("CreateEmployeeUow2");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmployeeUow2(EmpDbFirstRepoUowModel emp)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = await _eFCoreDBFirstRepoUowService.CreateEmployee2(emp);
                if (isCreated)
                {
                    return RedirectToAction("GetEmployeesUow2");
                }
            }

            return View("CreateEmployeeUow2");
        }

        public async Task<IActionResult> UpdateEmployeeUow2(int empId)
        {
            EmpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeById2(empId);
            return View("UpdateEmployeeUow2",emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployeeUow2(EmpDbFirstRepoUowModel emp)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = await _eFCoreDBFirstRepoUowService.UpdateEmployee2(emp);
                if (isUpdated)
                {
                    return RedirectToAction("GetEmployeesUow2");
                }
            }
            return View("UpdateEmployeeUow2", emp);
        }

        public async Task<IActionResult> DeleteEmployeeUow2(int empId)
        {
            EmpDbFirstRepoUowModel emp = await _eFCoreDBFirstRepoUowService.GetEmployeeById2(empId);
            return View("DeleteEmployeeUow2", emp);
        }

        [HttpPost, ActionName("DeleteEmployeeUow2")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUow2(EmpDbFirstRepoUowModel emp)
        {
            //if (ModelState.IsValid)
            //{
            bool isDeleted = await _eFCoreDBFirstRepoUowService.DeleteEmployee2(emp.EmpId);
            if (isDeleted)
            {
                return RedirectToAction("GetEmployeesUow2");
            }
            //}
            return View("DeleteEmployeeUow2", emp);
        }
        #endregion

        public async Task<IActionResult> GetSoftDelete()
        {
            var entity = new TblSoftdelete();
            using (var dbContext = new MyDBDbContext())
            {
                int id = 2;
                entity = await dbContext.TblSoftdelete.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            SoftDeleteModel model = new SoftDeleteModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                Phone = entity.Phone,
                IsDeleted = entity.IsDeleted
            };
            SoftDeleteVM vmModel = new SoftDeleteVM
            {
                SoftDeleteModel = model
            };
            return View(vmModel);
        }

        [HttpPost, ActionName("SoftDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SoftDeleteConfirmed(SoftDeleteVM vmModel)
        {
            //if (ModelState.IsValid)
            //{
            using (var dbContext = new MyDBDbContext())
            {
                var entity = await dbContext.TblSoftdelete.Where(x => x.Id == vmModel.SoftDeleteModel.Id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    dbContext.TblSoftdelete.Remove(entity);
                    await dbContext.SaveChangesAsync();
                }
            }
            //}
            return RedirectToAction("GetSoftDelete");
        }
    }
}
