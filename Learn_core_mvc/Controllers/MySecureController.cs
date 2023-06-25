using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class MySecureController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ServiceFilter(typeof(ActionEncodeFilter))]
        public IActionResult XssIndex(MyUser userData)
        {
            ViewBag.inputText = userData;
            return View("Index", userData);
        }

        [HttpPost]
        [ServiceFilter(typeof(ActionEncodeFilter))]
        public IActionResult Index([FromBody] MyUser userData)
        {
            return Json(userData);
        }

        public IActionResult CSRF()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CSRF(MyUser user)
        {
            ViewBag.PostedDataByCore = user;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CSRFDataByAjax([FromBody] MyUser user)
        {
            return Json(user);
        }

    }
}
