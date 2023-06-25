using Learn_core_mvc.Models;
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

        public IActionResult InnerJoin()
        {
            LinqInnerJoinVM linqInnerJoinVM = new LinqInnerJoinVM();
            linqInnerJoinVM.BookProperty = bookList;
            linqInnerJoinVM.OrderProperty = bookOrders;

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

        public IActionResult LeftJoin()
        {
            LinqLeftJoinVM linqLeftJoinVM = new LinqLeftJoinVM();
            linqLeftJoinVM.BookProperty = bookList;
            linqLeftJoinVM.OrderProperty = bookOrders;

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
    }
    
}
