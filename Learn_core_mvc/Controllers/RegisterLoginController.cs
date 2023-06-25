using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class RegisterLoginController : Controller
    {
        ISampleService sampleService;

        public RegisterLoginController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login user)
        {
            if (ModelState.IsValid)
            {
                var isLogin = await this.sampleService.LoginUser(user.UserEmail,user.UserPassword);
                if (isLogin)
                {
                    ViewBag.login = "Login done";
                }
                else
                {
                    ViewBag.login = "Can't login";
                }
            }
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Login user)
        {
            if (ModelState.IsValid)
            {
                var isRegister = await this.sampleService.RegisterUser(user);
                if (isRegister)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.register = "Can't register";
                    return View();
                }
            }
            return View();
        }
    }
}
