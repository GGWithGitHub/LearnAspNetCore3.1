using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class TestingController : Controller
    {
        static string maliciousData = null;

        static string myTestData = null;
        public IActionResult Index()
        {
            ViewBag.MaliciousData = maliciousData;
            return View();
        }

        [HttpPost]
        public IActionResult Index(string a)
        {
            maliciousData = a;
            return View();
        }

        [HttpPost]
        public IActionResult Fun(string check)
        {
            return View();
        }

        public IActionResult BackgroundWorkerTest()
        {
            ViewBag.TestData = myTestData;
            return View();
        }

        public IActionResult PostBackgroundWorkerTest(string myData)
        {
            Thread.Sleep(20000);
            myTestData = myData;
            return Json(new { success=true });
        }
    }
}
