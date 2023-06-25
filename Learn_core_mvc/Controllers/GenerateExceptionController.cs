using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class GenerateExceptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateError(int a,int b)
        {
            var c = a / b;
            return View();
        }
    }
}
