using Learn_core_mvc.Filters;
using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    [AllowAnonymous]
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
                return View("Index",model);
            }

            if (model.SessionLoginModel.UserEmail == "user@gmail.com" && model.SessionLoginModel.UserPassword == "1234")
            {
                HttpContext.Session.SetString("UserName", model.SessionLoginModel.UserEmail);
                HttpContext.Session.SetString("UserRole", "User");
            }
            else if (model.SessionLoginModel.UserEmail == "admin@gmail.com" && model.SessionLoginModel.UserPassword == "4567")
            {
                HttpContext.Session.SetString("UserName", model.SessionLoginModel.UserEmail);
                HttpContext.Session.SetString("UserRole", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "UserName or Password is incorrect");
                return View("Index", model);
            }
            return RedirectToAction("Dashboard");
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        //[ServiceFilter(typeof(SessionAuthorizationFilter))]
        [SessionAuthorizationFilter(Roles ="Admin")]
        public IActionResult Restricted()
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
