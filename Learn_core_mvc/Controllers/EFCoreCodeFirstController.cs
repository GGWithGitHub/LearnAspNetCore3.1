using AutoMapper;
using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository;
using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class EFCoreCodeFirstController : Controller
    {
        private readonly IEFCoreCodeFirstRepository _eFCoreCodeFirstRepo;
        private Mapper _studentMapper;
        public EFCoreCodeFirstController(IEFCoreCodeFirstRepository eFCoreCodeFirstRepo)
        {
            _eFCoreCodeFirstRepo = eFCoreCodeFirstRepo;

            var _configStudent = new MapperConfiguration(cfg => cfg.CreateMap<TblStudentCf, TblStudentCfModel>().ReverseMap());
            _studentMapper = new Mapper(_configStudent);
        }
        public async Task<IActionResult> GetStudents()
        {
            var studentsData = await _eFCoreCodeFirstRepo.GetStudents();
            var result = _studentMapper.Map<List<TblStudentCf>, List<TblStudentCfModel>>(studentsData);
            return View(result);
        }

        public async Task<IActionResult> CreateStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(TblStudentCfModel std)
        {
            if (ModelState.IsValid)
            {
                var student = _studentMapper.Map<TblStudentCfModel, TblStudentCf>(std);
                var result = await _eFCoreCodeFirstRepo.CreateStudent(student);
                if (result)
                {
                    return RedirectToAction("GetStudents");
                }
            }

            return View(std);
        }

        public async Task<IActionResult> UpdateStudent(int stdId)
        {
            var studentData = await _eFCoreCodeFirstRepo.GetStudentById(stdId);
            var result = _studentMapper.Map<TblStudentCf, TblStudentCfModel>(studentData);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStudent(TblStudentCfModel std)
        {
            if (ModelState.IsValid)
            {
                var student = _studentMapper.Map<TblStudentCfModel, TblStudentCf>(std);
                var result = await _eFCoreCodeFirstRepo.UpdateStudent(student);
                if (result)
                {
                    return RedirectToAction("GetStudents");
                }
            }
            return View(std);
        }

        public async Task<IActionResult> DeleteStudent(int stdId)
        {
            var studentData = await _eFCoreCodeFirstRepo.GetStudentById(stdId);
            var result = _studentMapper.Map<TblStudentCf, TblStudentCfModel>(studentData);
            return View(result);
        }

        [HttpPost, ActionName("DeleteStudent")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedStudent(TblStudentCfModel std)
        {
            var result = await _eFCoreCodeFirstRepo.DeleteStudent(std.StudentId);
            if (result)
            {
                return RedirectToAction("GetStudents");
            }
            return View(std);
        }

    }
}
