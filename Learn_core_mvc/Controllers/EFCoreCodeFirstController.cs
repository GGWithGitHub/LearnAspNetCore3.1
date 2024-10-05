using AutoMapper;
using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Models;
using Learn_core_mvc.Repository;
using Learn_core_mvc.Repository.EFCodeFirst.Models;
using Learn_core_mvc.ViewModels;
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
        private Mapper _studentFluentMapper;
        private Mapper _studentDetailsFluentMapper;
        private Mapper _evaluationFluentMapper;
        private Mapper _subjectFluentMapper;
        private Mapper _studentSubjectFluentMapper;
        private Mapper _stdStdDetailsFluentMapper;
        public EFCoreCodeFirstController(IEFCoreCodeFirstRepository eFCoreCodeFirstRepo)
        {
            _eFCoreCodeFirstRepo = eFCoreCodeFirstRepo;

            var _configStudent = new MapperConfiguration(cfg => cfg.CreateMap<TblStudentCf, TblStudentCfModel>().ReverseMap());
            _studentMapper = new Mapper(_configStudent);

            var _configStudentFluent = new MapperConfiguration(cfg => cfg.CreateMap<TblStudentFluentAPI, StudentFluentAPIModel>().ReverseMap());
            _studentFluentMapper = new Mapper(_configStudentFluent);
            
            var _configStudentDetailsFluent = new MapperConfiguration(cfg => cfg.CreateMap<TblStudentDetailsFluentAPI, StudentDetailsFluentAPIModel>().ReverseMap());
            _studentDetailsFluentMapper = new Mapper(_configStudentDetailsFluent);

            var _configEvaluation = new MapperConfiguration(cfg => cfg.CreateMap<TblEvaluationFluentAPI, EvaluationFluentAPIModel>().ReverseMap());
            _evaluationFluentMapper = new Mapper(_configEvaluation);

            var _configSubject = new MapperConfiguration(cfg => cfg.CreateMap<TblSubjectFluentAPI, SubjectFluentAPIModel>().ReverseMap());
            _subjectFluentMapper = new Mapper(_configSubject);

            var _configStdSub = new MapperConfiguration(cfg => cfg.CreateMap<TblStudentSubjectFluentAPI, StudentSubjectFluentAPIModel>().ReverseMap());
            _studentSubjectFluentMapper = new Mapper(_configStdSub);

            var _configStdStdDetail = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TblStudentFluentAPI, StdStdDetailsFluentAPIModel>()
                    .ForMember(dest => dest.StudentDetails, opt => opt.MapFrom(src => src.StudentDetails))
                    .ReverseMap()
                    .ForMember(dest => dest.StudentDetails, opt => opt.MapFrom(src => src.StudentDetails));

                cfg.CreateMap<TblStudentDetailsFluentAPI, StudentDetailsFluentAPIModel>()
                    .ReverseMap();
            });
            _stdStdDetailsFluentMapper = new Mapper(_configStdStdDetail);
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

        public async Task<IActionResult> FluentAPI()
        {
            FluentAPIVM model = new FluentAPIVM();

            var students = await _eFCoreCodeFirstRepo.GetTblStudentFluentAPI();
            var studentDetails = await _eFCoreCodeFirstRepo.GetTblStudentDetailsFluentAPI();
            var evalutions = await _eFCoreCodeFirstRepo.GetTblEvaluationFluentAPI();
            var subjects = await _eFCoreCodeFirstRepo.GetTblSubjectFluentAPI();
            var stdSubjects = await _eFCoreCodeFirstRepo.GetTblStudentSubjectFluentAPI();

            var stdRes = _studentFluentMapper.Map<List<TblStudentFluentAPI>, List<StudentFluentAPIModel>>(students);
            var stdDetailsRes = _studentDetailsFluentMapper.Map<List<TblStudentDetailsFluentAPI>, List<StudentDetailsFluentAPIModel>>(studentDetails);
            var evalRes = _evaluationFluentMapper.Map<List<TblEvaluationFluentAPI>, List<EvaluationFluentAPIModel>>(evalutions);
            var subRes = _subjectFluentMapper.Map<List<TblSubjectFluentAPI>, List<SubjectFluentAPIModel>>(subjects);
            var stdSubRes = _studentSubjectFluentMapper.Map<List<TblStudentSubjectFluentAPI>, List<StudentSubjectFluentAPIModel>>(stdSubjects);

            model.ListStudent = stdRes;
            model.ListStudentDetail = stdDetailsRes;
            model.ListEvaluation = evalRes;
            model.ListSubject = subRes;
            model.ListStudentSubject = stdSubRes;

            var stdStdDetails = await _eFCoreCodeFirstRepo.GetOneToOneData();
            var stdStdDetailsRes = _stdStdDetailsFluentMapper.Map<List<TblStudentFluentAPI>,List<StdStdDetailsFluentAPIModel>>(stdStdDetails);

            model.ListStdStdDetail = stdStdDetailsRes;

            return View(model);
        }

        public async Task<IActionResult> AddMainEntityWithRelatedEntity(StdStdDetailsFluentAPIModel model)
        {
            var stdStdDetailsMap = _stdStdDetailsFluentMapper.Map<StdStdDetailsFluentAPIModel, TblStudentFluentAPI>(model);
            var res = await _eFCoreCodeFirstRepo.AddMainEntityWithRelatedEntity(stdStdDetailsMap);
            return Json(new { success = true });
        }
    }
}
