using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class LoggingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateDivideByZeroError()
        {
            var a = 10;
            var b = 0;
            try
            {
                var c = a / b;
            }
            catch (Exception ex)
            {
                var file = @"F:\Gaurav_essential\ASP.NET CORE\Learn_core_mvc\Learn_core_mvc\AppLogs";
                ViewBag.Msg = $"Error is logged on the following file - {file}";
            }
            return View("Index");
        }

        public IActionResult GenerateCustomError()
        {
            try
            {
                throw new Exception("I am custom generated error.");
            }
            catch (Exception ex)
            {
                var file = @"F:\Gaurav_essential\ASP.NET CORE\Learn_core_mvc\Learn_core_mvc\AppLogs";
                ViewBag.Msg = $"Error is logged on the following file - {file}";
            }
            return View("Index");
        }
    }
}
