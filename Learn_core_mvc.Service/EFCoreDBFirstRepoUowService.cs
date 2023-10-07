using AutoMapper;
using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Repository;
using Learn_core_mvc.Repository.EFDBFirstRepo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Service
{
    public class EFCoreDBFirstRepoUowService : IEFCoreDBFirstRepoUowService
    {
        private readonly IEFCoreDBFirstUowRepository _eFCoreDBFirstUowRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork2 _unitOfWork2;
        private Mapper _EmployeeMapper;
        public EFCoreDBFirstRepoUowService(
            IUnitOfWork unitOfWork,
            IUnitOfWork2 unitOfWork2,
            IEFCoreDBFirstUowRepository eFCoreDBFirstUowRepository)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork2 = unitOfWork2;
            _eFCoreDBFirstUowRepository = eFCoreDBFirstUowRepository;

            var _configEmployee = new MapperConfiguration(cfg => cfg.CreateMap<TblEmployee, EmpDbFirstRepoUowModel>().ReverseMap());
            _EmployeeMapper = new Mapper(_configEmployee);
        }

        public async Task<List<EmpDbFirstRepoUowModel>> GetEmployees()
        {
            var employeesData = await _eFCoreDBFirstUowRepository.GetEmployees();
            List<EmpDbFirstRepoUowModel> employees = _EmployeeMapper.Map<List<TblEmployee>, List<EmpDbFirstRepoUowModel>>(employeesData);
            return employees;
        }

        public async Task<EmpDbFirstRepoUowModel> GetEmployeeById(int empId)
        {
            var employeeData = await _eFCoreDBFirstUowRepository.GetEmployeeById(empId);
            EmpDbFirstRepoUowModel employee = _EmployeeMapper.Map<TblEmployee, EmpDbFirstRepoUowModel>(employeeData);
            return employee;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            var res = await _eFCoreDBFirstUowRepository.DeleteEmployee(empId);
            _unitOfWork.Save();
            return res;
        }

        public async Task<bool> CreateEmployee(EmpDbFirstRepoUowModel emp)
        {
            TblEmployee employee = _EmployeeMapper.Map<EmpDbFirstRepoUowModel, TblEmployee>(emp);
            var res = await _eFCoreDBFirstUowRepository.CreateEmployee(employee);
            _unitOfWork.Save();
            return res;
        }

        public async Task<bool> UpdateEmployee(EmpDbFirstRepoUowModel emp)
        {
            TblEmployee employee = _EmployeeMapper.Map<EmpDbFirstRepoUowModel, TblEmployee>(emp);
            var res = await _eFCoreDBFirstUowRepository.UpdateEmployee(employee);
            _unitOfWork.Save();
            return res;
        }

        // -------------------------------------------------------------------------------------------------------------- //

        public async Task<List<EmpSpDbFirstRepoUowModel>> GetEmployeesSP()
        {
            var employeesData = await _eFCoreDBFirstUowRepository.GetEmployeesSP();
            return employeesData;
        }

        public async Task<EmpSpDbFirstRepoUowModel> GetEmployeeSP(int empId)
        {
            var employeeData = await _eFCoreDBFirstUowRepository.GetEmployeeByIdSP(empId);
            return employeeData;
        }

        public async Task<bool> DeleteEmployeeSP(int empId)
        {
            var res = await _eFCoreDBFirstUowRepository.DeleteEmployeeByIdSp(empId);
            return res;
        }

        public async Task<bool> CreateEmployeeSP(EmpSpDbFirstRepoUowModel emp)
        {
            var res = await _eFCoreDBFirstUowRepository.CreateEmployeeSp(emp);
            return res;
        }

        public async Task<bool> UpdateEmployeeSP(EmpSpDbFirstRepoUowModel emp)
        {
            var res = await _eFCoreDBFirstUowRepository.UpdateEmployeeSp(emp);
            return res;
        }

        // -------------------------------------------------------------------------------------------------------------- //

        public async Task<List<EmpDbFirstRepoUowModel>> GetEmployees2()
        {
            var employeesData = await _unitOfWork2.eFCoreDBFirstUowRepository2.GetEmployees();
            List<EmpDbFirstRepoUowModel> employees = _EmployeeMapper.Map<List<TblEmployee>, List<EmpDbFirstRepoUowModel>>(employeesData);
            return employees;
        }

        public async Task<EmpDbFirstRepoUowModel> GetEmployeeById2(int empId)
        {
            var employeeData = await _unitOfWork2.eFCoreDBFirstUowRepository2.GetEmployeeById(empId);
            EmpDbFirstRepoUowModel employee = _EmployeeMapper.Map<TblEmployee, EmpDbFirstRepoUowModel>(employeeData);
            return employee;
        }

        public async Task<bool> DeleteEmployee2(int empId)
        {
            var res = await _unitOfWork2.eFCoreDBFirstUowRepository2.DeleteEmployee(empId);
            _unitOfWork2.Save();
            return res;
        }

        public async Task<bool> CreateEmployee2(EmpDbFirstRepoUowModel emp)
        {
            TblEmployee employee = _EmployeeMapper.Map<EmpDbFirstRepoUowModel, TblEmployee>(emp);
            var res = await _unitOfWork2.eFCoreDBFirstUowRepository2.CreateEmployee(employee);
            _unitOfWork2.Save();
            return res;
        }

        public async Task<bool> UpdateEmployee2(EmpDbFirstRepoUowModel emp)
        {
            TblEmployee employee = _EmployeeMapper.Map<EmpDbFirstRepoUowModel, TblEmployee>(emp);
            var res = await _unitOfWork2.eFCoreDBFirstUowRepository2.UpdateEmployee(employee);
            _unitOfWork2.Save();
            return res;
        }
    }
}
