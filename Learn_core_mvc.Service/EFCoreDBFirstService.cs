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
    public class EFCoreDBFirstService : IEFCoreDBFirstService
    {
        private readonly IEFCoreDBFirstRepository _eFCoreDBFirstRepository;
        private Mapper _EmployeeMapper;
        public EFCoreDBFirstService(IEFCoreDBFirstRepository eFCoreDBFirstRepository)
        {
            _eFCoreDBFirstRepository = eFCoreDBFirstRepository;

            var _configEmployee = new MapperConfiguration(cfg => cfg.CreateMap<TblEmployee, EmployeeForEFCoreDbFirst>().ReverseMap());
            _EmployeeMapper = new Mapper(_configEmployee);
        }

        public async Task<List<EmployeeForEFCoreDbFirst>> GetEmployees()
        {
            var employeesData = await _eFCoreDBFirstRepository.GetEmployees();
            List<EmployeeForEFCoreDbFirst> employees = _EmployeeMapper.Map<List<TblEmployee>, List<EmployeeForEFCoreDbFirst>>(employeesData);
            return employees;
        }

        public async Task<EmployeeForEFCoreDbFirst> GetEmployeeById(int empId)
        {
            var employeeData = await _eFCoreDBFirstRepository.GetEmployeeById(empId);
            EmployeeForEFCoreDbFirst employee = _EmployeeMapper.Map<TblEmployee, EmployeeForEFCoreDbFirst>(employeeData);
            return employee;
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            return await _eFCoreDBFirstRepository.DeleteEmployee(empId);
        }

        public async Task<bool> CreateEmployee(EmployeeForEFCoreDbFirst emp)
        {
            TblEmployee employee = _EmployeeMapper.Map<EmployeeForEFCoreDbFirst, TblEmployee>(emp);
            return await _eFCoreDBFirstRepository.CreateEmployee(employee);
        }

        public async Task<bool> UpdateEmployee(EmployeeForEFCoreDbFirst emp)
        {
            TblEmployee employee = _EmployeeMapper.Map<EmployeeForEFCoreDbFirst, TblEmployee>(emp);
            return await _eFCoreDBFirstRepository.UpdateEmployee(employee);
        }
    }
}
