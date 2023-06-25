using Learn_core_mvc.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class MailController : Controller
    {
        ISampleService sampleService;
        public MailController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SendMail(string toMail)
        {
            var status = await this.sampleService.SendMail(toMail);
            if (status)
            {
                TempData["mail"] = "Mail is sent.";
            }
            else
            {
                TempData["mail"] = "Mail is not sent.";
            }
            return RedirectToAction("Index");
        }
    }
}
