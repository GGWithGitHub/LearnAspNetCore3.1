using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class LoggingController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public LoggingController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
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

        public IActionResult LogErrInFile()
        {
            return View();
        }

        public IActionResult CustomLogErrInFile(int firstValue,int secondValue)
        {
            try
            {
                var res = firstValue / secondValue;
            }
            catch (Exception ex)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = "Error.txt";
                var dir = Path.Combine(wwwRootPath, "LogErrors");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                var filePath = Path.Combine(dir, fileName);

                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                var stackTrace = ex.StackTrace;
                var logTime = DateTime.Now;
                var fullErrMsg = "\n------------Start-----------";
                fullErrMsg += $"\nMessage: {errorMessage} \nStackTrace: {stackTrace} \nTime: {logTime}";
                fullErrMsg += "\n------------End-----------";

                System.IO.File.AppendAllText(filePath, fullErrMsg);
            }
            return View("LogErrInFile");
        }

        public IActionResult LogUsingSerilog()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult PostLogUsingSerilog()
        {
            var a = 0;
            var b = 10;
            var c = b / a;
            return RedirectToAction("LogUsingSerilog");
        }
    }
}
