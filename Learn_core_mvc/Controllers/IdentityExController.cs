using Learn_core_mvc.Models;
using Learn_core_mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class IdentityExController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityUserService _identityUserService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public IdentityExController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IIdentityUserService identityUserService,
            IEmailService emailService,
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityUserService = identityUserService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(SignupModelIdentity signupModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = signupModel.UserEmail,
                    UserName = signupModel.UserEmail
                };
                var result = await _userManager.CreateAsync(user, signupModel.UserPassword);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    if (!string.IsNullOrEmpty(token))
                    {
                        await SendEmailConfirmationEmail(user, token);
                        ViewBag.UserCreated = "User signup done";
                    }
                }
                else
                {
                    foreach (var errMsg in result.Errors)
                    {
                        ModelState.AddModelError("", errMsg.Description);
                    }
                }
            }
            
            return View("Signup", signupModel);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token, string email)
        {
            token = token.Replace(' ', '+');
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                var result = await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
                if (result.Succeeded)
                {
                    ViewBag.EmailVerified = true;
                }
            }

            return View();
        }

        private async Task SendEmailConfirmationEmail(IdentityUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(SignInModelIdentity signInModel)
        {
            if (ModelState.IsValid)
            {
                bool lockoutOnFailure = true;
                var result = await _signInManager.PasswordSignInAsync(signInModel.UserEmail, signInModel.UserPassword, signInModel.IsRemember, lockoutOnFailure);
                if (result.Succeeded)
                {
                    return RedirectToActionPermanent("Home");
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "User not allowed to login, please confirm link on email");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked, Try after 1 min");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }

            return View("Login", signInModel);
        }

        [Authorize]
        public IActionResult Home()
        {
            ViewBag.UserName = _identityUserService.GetUserName();
            ViewBag.UserId = _identityUserService.GetUserId();
            return View();
        }

        [Authorize]
        public IActionResult PublicPage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AuthorizePage()
        {
            return View();
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModelIdentity changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var userId = _identityUserService.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                var result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var errMsg in result.Errors)
                {
                    ModelState.AddModelError("", errMsg.Description);
                }
            }
            
            return View("ChangePassword", changePasswordModel);
        }

        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "IdentityEx");
        }
    }
}
