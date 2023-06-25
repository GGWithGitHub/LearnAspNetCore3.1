using Learn_core_mvc.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class ClaimBaseAuth : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LogIn(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            else
            {
                var user = GetUserData(login.UserEmail);
                if (user != null)
                {
                    bool isRememberMe = login.RememberMe;
                    await SetAuthCookie(login.UserEmail, isRememberMe, new List<string>() { user.UserRole });
                    return RedirectToAction("Home", "ClaimBaseAuth", null);
                }
                else
                {
                    return View("Index");
                }
            }
        }

        public IActionResult Home()
        {
            return View();
        }

        [Authorize(Roles = "User1")]
        public IActionResult User1()
        {
            return View();
        }

        [Authorize(Roles = "User2")]
        public IActionResult User2()
        {
            return View();
        }
        public IActionResult LogOff()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "ClaimBaseAuth", null);
        }

        public Login GetUserData(string email)
        {
            if (email == "user1@gmail.com")
            {
                return new Login { UserId = 1, UserEmail = "user1@gmail.com", UserPassword = "111111", UserRole = "User1" };
            }
            else if (email == "user2@gmail.com")
            {
                return new Login { UserId = 2, UserEmail = "user2@gmail.com", UserPassword = "111111", UserRole = "User2" };
            }
            else
            {
                return null;
            }
        }

        private async Task SetAuthCookie(string UserEmail, bool persist, List<string> roles = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, UserEmail)
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    if (role != null)
                        claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principle = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties()
            {
                IsPersistent = persist,
                ExpiresUtc = DateTime.UtcNow.AddYears(2)
            };

            await HttpContext.SignInAsync(principle, authenticationProperties);
        }
    }
}
