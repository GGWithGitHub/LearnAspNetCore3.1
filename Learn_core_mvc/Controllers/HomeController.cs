using Learn_core_mvc.Models;
using Learn_core_mvc.Service;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ISampleService _sampleService;
        public HomeController(ILogger<HomeController> logger, ISampleService sampleService)
        {
            _logger = logger;
            _sampleService = sampleService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ExceptionGenerator()
        {
            var a = 10; var b = 0;
            var c = a / b;
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ErrorViewModel errorViewModel = new ErrorViewModel();
            errorViewModel.ErrorMessage = exceptionFeature.Error.Message;
            errorViewModel.Path = exceptionFeature.Path;
            errorViewModel.StackTrace = exceptionFeature.Error.StackTrace;
            _sampleService.AddException(exceptionFeature);
            return View(errorViewModel);
        }
    }
}
