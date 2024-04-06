using Learn_core_mvc.IdentityDbContextFolder;
using Learn_core_mvc.Models;
using Learn_core_mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class IdentityExController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IIdentityUserService _identityUserService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public IdentityExController(
            AppDbContext appDbContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IIdentityUserService identityUserService,
            IEmailService emailService,
            IConfiguration configuration
        )
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _identityUserService = identityUserService;
            _emailService = emailService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Signup()
        {
            SignupModelIdentity model = new SignupModelIdentity();
            var roles = await _roleManager.Roles.ToListAsync();
            roles.ForEach(item =>
            {
                model.UserRoles.Add(new SelectListItem
                {
                    Text = item.NormalizedName,
                    Value = item.Id
                });
            });
            return View(model);
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
                    //var roleResult = await _userManager.AddToRoleAsync(user, signupModel.RoleName);
                    var userRole = new IdentityUserRole<string> { RoleId = signupModel.RoleId, UserId = user.Id };
                    await _appDbContext.Set<IdentityUserRole<string>>().AddAsync(userRole);
                    await _appDbContext.SaveChangesAsync();


                    ModelState.Clear();
                    return RedirectToAction("ReSendConfirmEmail", new { email = signupModel.UserEmail });
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
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
        {
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
                if (result.Succeeded)
                {
                    ViewBag.EmailVerified = true;
                }
            }

            return View();
        }

        public async Task<IActionResult> ReSendConfirmEmail(string email)
        {
            EmailConfirmModel model = new EmailConfirmModel
            {
                Email = email
            };

            var user = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReSendConfirmEmail(EmailConfirmModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if (!string.IsNullOrEmpty(token))
                {
                    await SendEmailConfirmationEmail(user, token);
                }
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong.");
            }
            return View(model);
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

        public IActionResult AddColumnsToAspNetUsersTable()
        {
            return View();
        }

        [AllowAnonymous, HttpGet("fotgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous, HttpPost("fotgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                // code here
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    if (!string.IsNullOrEmpty(token))
                    {
                        await SendForgotPasswordEmail(user, token);

                        ModelState.Clear();
                        model.EmailSent = true;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You are not registered with us");
                }
            }
            return View(model);
        }

        private async Task SendForgotPasswordEmail(IdentityUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;

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

            await _emailService.SendEmailForForgotPassword(options);
        }

        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordIdentityModel resetPasswordModel = new ResetPasswordIdentityModel
            {
                Token = token,
                UserId = uid
            };
            return View(resetPasswordModel);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordIdentityModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var user = await _userManager.FindByIdAsync(model.UserId);
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
