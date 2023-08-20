using Learn_core_mvc.Filters;
using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            SessionVM sessionVM = new SessionVM();
            return View(sessionVM);
        }

        [HttpPost]
        public IActionResult Index(SessionVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            if (model.SessionLoginModel.UserEmail == "user@gmail.com" && model.SessionLoginModel.UserPassword == "111111")
            {
                HttpContext.Session.SetString("UserName", model.SessionLoginModel.UserEmail);
            }
            else
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
            }
            return View(model);
        }

        public IActionResult Home()
        {
            return View();
        }

        //[Session]
        [ServiceFilter(typeof(SessionAuthorizationFilter))]
        public IActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");

            return RedirectToAction("Index");
        }
    }
}
