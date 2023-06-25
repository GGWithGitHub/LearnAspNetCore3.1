using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class StaticFileSampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
