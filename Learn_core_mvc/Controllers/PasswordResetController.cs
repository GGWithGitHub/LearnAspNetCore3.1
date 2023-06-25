using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class PasswordResetController : Controller
    {
        ISampleService sampleService;

        public PasswordResetController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login user)
        {
            if (ModelState.IsValid)
            {
                var isLogin = await this.sampleService.LoginUser(user.UserEmail, user.UserPassword);
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

        public IActionResult Register()
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
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.register = "Can't register";
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RequestPasswordReset()
        {
            return View(new RequestPasswordResetModel());
        }

        [HttpPost]
        public async Task<IActionResult> RequestPasswordReset(RequestPasswordResetModel requestPasswordResetModel)
        {
            var submitted = false;

            if (this.ModelState.IsValid)
            {
                var matchingUser = await this.sampleService.GetUserByEmail(requestPasswordResetModel.Email);

                if (matchingUser != null)
                {
                    matchingUser.Token = Guid.NewGuid();

                    if (await this.sampleService.UpdateUserToken(matchingUser.UserId, matchingUser.Token))
                    {
                        var isSuccessful = await this.sampleService.PasswordResetNotificationOnMail(matchingUser.UserEmail, matchingUser.Token);
                        submitted = isSuccessful;
                    }
                }
                else
                {
                    ViewBag.EmailNotAvailable = "Entered email is not available in database";
                }
            }

            if (submitted)
            {
                this.ViewData["Email"] = requestPasswordResetModel.Email;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(Guid token)
        {
            var matchingUser = await this.sampleService.GetUserByToken(token);

            if (matchingUser != null)
            {
                var resetPasswordModel = new ResetPasswordModel()
                {
                    Token = token
                };

                return View(resetPasswordModel);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            var isSuccessful = false;

            if (this.ModelState.IsValid)
            {
                var matchingUser = await this.sampleService.GetUserByToken(resetPasswordModel.Token);

                if (matchingUser != null)
                {
                    if (await this.sampleService.UpdateUserPassword(matchingUser.UserId, matchingUser.UserSalt, resetPasswordModel.Password))
                    {
                        isSuccessful = true;
                    }
                }
            }

            this.ViewData["IsSuccessful"] = isSuccessful;

            return View();
        }
    }
}
