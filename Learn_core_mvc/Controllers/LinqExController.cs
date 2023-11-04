﻿using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class LinqExController : Controller
    {
        List<LinqTestEmployee> employees = new List<LinqTestEmployee>(){
            new LinqTestEmployee(){ Id=1, Name="Tom", Email="tom@gmail.com" },
            new LinqTestEmployee(){ Id=2, Name="John", Email="john@gmail.com" },
            new LinqTestEmployee(){ Id=3, Name="Mark", Email="mark@gmail.com" },
            new LinqTestEmployee(){ Id=4, Name="Kim", Email="kim@gmail.com" },
            new LinqTestEmployee(){ Id=5, Name="John", Email="john22@gmail.com" }
        };

        List<LinqBook> bookList = new List<LinqBook>
        {
            new LinqBook{BookID=1, BookNm="DevCurry.com Developer Tips"},
            new LinqBook{BookID=2, BookNm=".NET and COM for Newbies"},
            new LinqBook{BookID=3, BookNm="51 jQuery ASP.NET Recipes"},
            new LinqBook{BookID=4, BookNm="Motivational Gurus"},
            new LinqBook{BookID=5, BookNm="Spiritual Gurus"}
        };

        List<LinqOrder> bookOrders = new List<LinqOrder>{
            new LinqOrder{OrderID=1, BookID=1, PaymentMode="Cheque"},
            new LinqOrder{OrderID=2, BookID=5, PaymentMode="Credit"},
            new LinqOrder{OrderID=3, BookID=1, PaymentMode="Cash"},
            new LinqOrder{OrderID=4, BookID=3, PaymentMode="Cheque"},
            new LinqOrder{OrderID=5, BookID=3, PaymentMode="Cheque"},
            new LinqOrder{OrderID=6, BookID=4, PaymentMode="Cash"}
        };
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FirstFirstOrDefaultSingleSingleOrDefault()
        {
            return View();
        }
        public IActionResult IndexForFirst(string empname)
        {
            try
            {
                var firstResult = employees.First(x => x.Name == empname);
                if (firstResult != null)
                {
                    var emp = JsonConvert.SerializeObject(firstResult);
                    ViewBag.result = "Success -> "+ emp;
                }
                else
                {
                    ViewBag.result = "Success -> null";
                }
            }
            catch (Exception ex)
            {
                ViewBag.result = "Exception -> " + ex.Message;
            }
            
            return View("FirstFirstOrDefaultSingleSingleOrDefault");
        }

        public IActionResult IndexForFirstOrDefault(string empname)
        {
            try
            {
                var FirstOrDefaultResult = employees.FirstOrDefault(x => x.Name == empname);
                if (FirstOrDefaultResult != null)
                {
                    var emp = JsonConvert.SerializeObject(FirstOrDefaultResult);
                    ViewBag.result = "Success -> " + emp;
                }
                else
                {
                    ViewBag.result = "Success -> null";
                }
            }
            catch (Exception ex)
            {
                ViewBag.result = "Exception -> " + ex.Message;
            }

            return View("FirstFirstOrDefaultSingleSingleOrDefault");
        }

        public IActionResult IndexForSingle(string empname)
        {
            try
            {
                var SingleResult = employees.Single(x => x.Name == empname);
                if (SingleResult != null)
                {
                    var emp = JsonConvert.SerializeObject(SingleResult);
                    ViewBag.result = "Success -> " + emp;
                }
                else
                {
                    ViewBag.result = "Success -> null";
                }
            }
            catch (Exception ex)
            {
                ViewBag.result = "Exception -> " + ex.Message;
            }

            return View("FirstFirstOrDefaultSingleSingleOrDefault");
        }

        public IActionResult IndexForSingleOrDefault(string empname)
        {
            try
            {
                var SingleOrDefaultResult = employees.SingleOrDefault(x => x.Name == empname);
                if (SingleOrDefaultResult != null)
                {
                    var emp = JsonConvert.SerializeObject(SingleOrDefaultResult);
                    ViewBag.result = "Success -> " + emp;
                }
                else
                {
                    ViewBag.result = "Success -> null";
                }
            }
            catch (Exception ex)
            {
                ViewBag.result = "Exception -> " + ex.Message;
            }

            return View("FirstFirstOrDefaultSingleSingleOrDefault");
        }

        public IActionResult SelectSelectMany()
        {
            var students = LinqStudent.GetAllStudetns();
            return View(students);
        }

        public IActionResult GroupByMultipleKeys()
        {
            //By method linq syntax:
            var employeeGroups = LinqEmployee2.GetAllEmployees()
                                        .GroupBy(x => new { x.Department, x.Gender })
                                        .OrderBy(g => g.Key.Department).ThenBy(g => g.Key.Gender)
                                        .Select(g => new GrpMulKeyModel
                                        {
                                            Dept = g.Key.Department,
                                            Gender = g.Key.Gender,
                                            Employees = g.OrderBy(x => x.Name).ToList()
                                        }).ToList();

            //By query linq syntax:
            employeeGroups = (from employee in LinqEmployee2.GetAllEmployees()
                             group employee by new
                             {
                                 employee.Department,
                                 employee.Gender
                             } into eGroup
                             orderby eGroup.Key.Department ascending,
                                           eGroup.Key.Gender ascending
                             select new GrpMulKeyModel
                             {
                                 Dept = eGroup.Key.Department,
                                 Gender = eGroup.Key.Gender,
                                 Employees = eGroup.OrderBy(x => x.Name).ToList()
                             }).ToList();
            return View(employeeGroups);
        }

        public IActionResult InnerJoin()
        {
            LinqInnerJoinVM linqInnerJoinVM = new LinqInnerJoinVM();
            linqInnerJoinVM.BookProperty = bookList;
            linqInnerJoinVM.OrderProperty = bookOrders;

            //Using Method Syntax
            linqInnerJoinVM.BookOrderProperty = bookList.Join(bookOrders,
                bken => bken.BookID, bkOen => bkOen.BookID,
                (bken, bkOen) => new { bken, bkOen })
                .Select(sel => new LinqBookOrder {
                    BookID = sel.bken.BookID,
                    BookNm = sel.bken.BookNm,
                    PaymentMode = sel.bkOen.PaymentMode
                }).ToList();

            //Using Query Syntax
            linqInnerJoinVM.BookOrderProperty = (from bk in bookList
                                join ordr in bookOrders
                                on bk.BookID equals ordr.BookID
                                select new LinqBookOrder
                                {
                                    BookID= bk.BookID,
                                    BookNm = bk.BookNm,
                                    PaymentMode = ordr.PaymentMode
                                }).ToList();

            return View(linqInnerJoinVM);
        }

        public IActionResult InnerJoinOnMultipleTable()
        {
            LinqInnerJoinMulTblVM linqInnerJoinMulTblVM = new LinqInnerJoinMulTblVM();
            linqInnerJoinMulTblVM.DepartmentsProperty = LinqDepartment.GetAllDepartments();
            linqInnerJoinMulTblVM.EmployeesProperty = LinqEmployee.GetAllEmployees();
            linqInnerJoinMulTblVM.AddressesProperty = LinqAddress.GetAllAddress();
            linqInnerJoinMulTblVM.CountriesProperty = LinqCountry.GetAllCountries();

            linqInnerJoinMulTblVM.DepEmpAddrCounProperty = (from d in LinqDepartment.GetAllDepartments()
                                                            join e in LinqEmployee.GetAllEmployees() on d.ID equals e.DepartmentID
                                                            join a in LinqAddress.GetAllAddress() on e.AddressId equals a.Id
                                                            join c in LinqCountry.GetAllCountries() on a.CountryId equals c.Id
                                                            select new LinqDepEmpAddrCoun
                                                            {
                                                                DepartmentId = d.ID,
                                                                DepartmentName = d.Name,
                                                                EmployeeId = e.ID,
                                                                EmployeeName = e.Name,
                                                                AddressId = a.Id,
                                                                AddressName = a.Address,
                                                                CountryId = c.Id,
                                                                CountryName = c.Name
                                                            }).ToList();

            return View(linqInnerJoinMulTblVM);
        }

        public IActionResult LeftJoin()
        {
            LinqLeftJoinVM linqLeftJoinVM = new LinqLeftJoinVM();
            linqLeftJoinVM.BookProperty = bookList;
            linqLeftJoinVM.OrderProperty = bookOrders;

            //Using Method Syntax
            linqLeftJoinVM.BookOrderProperty = bookList.GroupJoin(bookOrders,
                bken => bken.BookID, bkOen => bkOen.BookID,
                (bken, bkOen) => new { bken, bkOen })
                .SelectMany(sel => sel.bkOen.DefaultIfEmpty(new LinqOrder()),
                (selbken, selbkOen) => new LinqBookOrder
                {
                    BookID = selbken.bken.BookID,
                    BookNm = selbken.bken.BookNm,
                    PaymentMode = selbkOen.PaymentMode
                }).ToList();

            //Using Query Syntax
            linqLeftJoinVM.BookOrderProperty = (from bk in bookList
                                                 join ordr in bookOrders
                                                 on bk.BookID equals ordr.BookID
                                                 into a
                                                 from b in a.DefaultIfEmpty(new LinqOrder())
                                                 select new LinqBookOrder
                                                 {
                                                     BookID = bk.BookID,
                                                     BookNm = bk.BookNm,
                                                     PaymentMode = b.PaymentMode
                                                 }).ToList();

            return View(linqLeftJoinVM);
        }

        public IActionResult GroupJoin()
        {
            LinqGroupJoinVM linqGroupJoinVM = new LinqGroupJoinVM();
            linqGroupJoinVM.DepartmentsProperty = LinqDepartment.GetAllDepartments();
            linqGroupJoinVM.EmployeesProperty = LinqEmployee.GetAllEmployees();

            linqGroupJoinVM.DepartmentEmployeeProperty = (from d in LinqDepartment.GetAllDepartments()
                                                            join e in LinqEmployee.GetAllEmployees()
                                                            on d.ID equals e.DepartmentID into eGroup
                                                            select new LinqDepartmentEmployee
                                                            {
                                                                Department = d,
                                                                Employees = eGroup.ToList()
                                                            }).ToList();
            return View(linqGroupJoinVM);
        }

        public IActionResult DistinctRecForCmplxType()
        {
            List<DistRecForCompTypeModel> list = new List<DistRecForCompTypeModel>()
            {
                new DistRecForCompTypeModel { Id = 101, Name = "Mike"},
                new DistRecForCompTypeModel { Id = 101, Name = "Mike"},
                new DistRecForCompTypeModel { Id = 102, Name = "Mary"}
            };

            var result = list.Select(x => new { x.Id, x.Name }).Distinct()
                        .Select(x=>new DistRecForCompTypeModel { 
                            Id = x.Id,
                            Name = x.Name
                        }).ToList();
            return View(result);
        }
    }
    
}
