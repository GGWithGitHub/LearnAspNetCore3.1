using Learn_core_mvc.IdentityDbContextFolder;
using Learn_core_mvc.Models;
using Learn_core_mvc.Services;
using Learn_core_mvc.ViewModels;
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

        private async Task SendTwoFactorTokenEmail(IdentityUser user, string token)
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{Token}}",token)
                }
            };

            await _emailService.SendTwoFactorToken(options);
        }

        private async Task SendTwoFactorEnableDisableEmail(IdentityUser user, string token)
        {
            string actionMessage = user.TwoFactorEnabled ? "Disable Two-Factor Authentication" : "Enable Two-Factor Authentication";
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.Email),
                    new KeyValuePair<string, string>("{{EnableDisable2FAMsg}}", actionMessage),
                    new KeyValuePair<string, string>("{{Token}}",token)
                }
            };
            await _emailService.SendEmailTwoFactorEnableDisable(options);
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
                var user = await _userManager.FindByEmailAsync(signInModel.UserEmail);

                bool lockoutOnFailure = true;
                var result = await _signInManager.PasswordSignInAsync(signInModel.UserEmail, signInModel.UserPassword, signInModel.IsRemember, lockoutOnFailure);
                if (result.Succeeded)
                {
                    return RedirectToActionPermanent("Home");
                }
                else if (result.RequiresTwoFactor)
                {
                    var TwoFactorAuthenticationToken = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

                    // Send SMS if the phone number is confirmed
                    if (!string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumberConfirmed)
                    {
                        //var smsMessage = $"Your Two-Factor Authentication code is: {TwoFactorAuthenticationToken}. Please use this code to log in.";
                        //await smsSender.SendSmsAsync(user.PhoneNumber, smsMessage);
                    }

                    // Send Email if the email is confirmed
                    if (!string.IsNullOrEmpty(user.Email) && user.EmailConfirmed)
                    {
                        await SendTwoFactorTokenEmail(user, TwoFactorAuthenticationToken);
                    }

                    return RedirectToAction("VerifyTwoFactorToken", "IdentityEx", new { email=signInModel.UserEmail, rememberMe=signInModel.IsRemember, twoFactorAuthenticationToken=TwoFactorAuthenticationToken });
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
        [HttpGet]
        public async Task<IActionResult> ManageTwoFactorAuthentication()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "IdentityEx");
            }
            // First, ensure the user's email and phone number are confirmed
            //if (!user.PhoneNumberConfirmed || !user.EmailConfirmed)
            if (!user.EmailConfirmed)
            {
                ViewBag.ErrorTitle = "Two-Factor Authentication Setup Error";
                ViewBag.ErrorMessage = "You cannot enable or disable Two-Factor Authentication because your email is not yet confirmed.";
                return RedirectToAction("Login", "IdentityEx");
            }
            // Determine the action (Enable or Disable 2FA)
            string actionMessage = user.TwoFactorEnabled ? "Disable Two-Factor Authentication" : "Enable Two-Factor Authentication";
            // Generate the Two-Factor Authentication Token
            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            // Send the token via SMS if the phone number is confirmed
            if (!string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumberConfirmed)
            {
                //string smsMessage = $"Your security code to {actionMessage.ToLower()} is: {twoFactorToken}. Please do not share this code with anyone.";
                //await smsSender.SendSmsAsync(user.PhoneNumber, smsMessage);
            }
            // Send the token via Email if the email is confirmed
            if (!string.IsNullOrEmpty(user.Email) && user.EmailConfirmed)
            {
                await SendTwoFactorEnableDisableEmail(user, twoFactorToken);
            }
            ViewBag.TwoFAToken = twoFactorToken;
            // Notify the user that the token has been sent
            return View(); // View for the user to enter the token
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ManageTwoFactorAuthentication(TwoFactorTokenVM model)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please enter a valid security code.";
                return View(model);
            }
            // Fetch the user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["ErrorMessage"] = "User session expired. Please log in again.";
                return RedirectToAction("Login", "IdentityEx");
            }
            // Verify the token
            var isTokenValid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider, model.TwoFactorToken);
            if (isTokenValid)
            {
                // Toggle Two-Factor Authentication status
                user.TwoFactorEnabled = !user.TwoFactorEnabled;
                await _userManager.UpdateAsync(user);
                // Set a success message
                TempData["SuccessMessage"] = user.TwoFactorEnabled
                    ? "Two-Factor Authentication has been successfully enabled for your account."
                    : "Two-Factor Authentication has been successfully disabled for your account.";
                return RedirectToAction("TwoFactorAuthenticationSuccessful");
            }
            // Handle invalid token
            TempData["ErrorMessage"] = "The security code is invalid or has expired. Please try again.";
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult TwoFactorAuthenticationSuccessful()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult VerifyTwoFactorToken(string email, bool rememberMe, string twoFactorAuthenticationToken)
        {
            VerifyTwoFactorTokenVM model = new VerifyTwoFactorTokenVM()
            {
                RememberMe = rememberMe,
                Email = email,
                Token = twoFactorAuthenticationToken
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyTwoFactorToken(VerifyTwoFactorTokenVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                return View(model);
            }

            // Validate the 2FA token
            var result = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", model.TwoFactorCode);
            if (result)
            {
                // Sign in the user and redirect
                await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                // Redirect to default page
                return RedirectToAction("Home", "IdentityEx");
            }

            ModelState.AddModelError(string.Empty, "Invalid verification code.");
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ResendTwoFactorToken(string Email, string ReturnUrl, bool RememberMe)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return RedirectToAction("Login", "IdentityEx");
            }

            // Generate a 2FA token either using DefaultPhoneProvider or DefaultEmailProvider
            // Which provider we use here, same we need to use while doing the verification
            var TwoFactorAuthenticationToken = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultEmailProvider);
            //var TwoFactorAuthenticationToken3 = await userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);

            // Send SMS if the phone number is confirmed
            if (!string.IsNullOrEmpty(user.PhoneNumber) && user.PhoneNumberConfirmed)
            {
                //var smsMessage = $"Your Two-Factor Authentication code is: {TwoFactorAuthenticationToken}. Please use this code to log in.";
                //await smsSender.SendSmsAsync(user.PhoneNumber, smsMessage);
            }

            // Send Email if the email is confirmed
            if (!string.IsNullOrEmpty(user.Email) && user.EmailConfirmed)
            {
                await SendTwoFactorTokenEmail(user, TwoFactorAuthenticationToken);
            }

            // Redirect to Two-Factor Authentication verification page with data
            return RedirectToAction("VerifyTwoFactorToken", "IdentityEx", new { Email, ReturnUrl, RememberMe });
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
