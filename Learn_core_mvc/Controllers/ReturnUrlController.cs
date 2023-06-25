using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class ReturnUrlController : Controller
    {
        public IActionResult Home()
        {
            ViewBag.LoginLogout = HttpContext.Session.GetString("LoginSession");
            return View();
        }
        public IActionResult Dream()
        {
            var session = HttpContext.Session.GetString("LoginSession");
            if (session!=null)
            {
                ViewBag.LoginLogout = HttpContext.Session.GetString("LoginSession");
                return View();
            }
            var returnUrl = "Dream";
            return RedirectToAction("Login", new { returnUrl = returnUrl });
        }

        public IActionResult Login(string returnUrl=null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult LogInPost()
        {
            var form = Request.Form;
            if (form["user_email"] == "Test@gmail.com" && form["user_pass"]== "12345")
            {
                HttpContext.Session.SetString("LoginSession", "Done Login");
                if (form["user_return_url"] != "")
                {
                    return RedirectToAction(form["user_return_url"]);
                }
                return RedirectToAction("Home");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoginSession");
            return RedirectToAction("Home");
        }
    }
}
