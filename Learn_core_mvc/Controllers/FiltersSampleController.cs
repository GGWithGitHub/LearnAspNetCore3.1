using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class FiltersSampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CustAuthFilter()
        {
            return View();
        }

        [Attributes.Authorize("Read")]
        public IActionResult ReadAuth()
        {
            return View();
        }

        [Attributes.Authorize("Write")]
        public IActionResult WriteAuth()
        {
            return View();
        }

        public IActionResult CustActionFilter()
        {
            FiltersBookModel filtersBookModel = new FiltersBookModel();
            return View(filtersBookModel);
        }

        [Attributes.ValidateModel]
        [HttpPost]
        public IActionResult CreateBook([FromForm] FiltersBookModel filtersBookModel)
        {
            return View("CustActionFilter");
        }

        public IActionResult CustResourceFilter()
        {
            return View();
        }

        [Attributes.CacheResource]
        public IActionResult GetLargeData()
        {
            string data = "We conatin large amount of data"; // here you can get data from database
            return Json(new { status = true, data = data });
        }

        public IActionResult GetCachedData(string data)
        {
            return Json(new { status = true, data = data });
        }
    }
}
