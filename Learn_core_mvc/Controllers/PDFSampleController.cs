using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class PDFSampleController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PDFSampleController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContentForPDFPV()
        {
            return PartialView("_ContentForPDFPV");
        }

        public IActionResult GeneratePDF()
        {
            return new ViewAsPdf("GeneratePDF")
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }

        public IActionResult GeneratePDF2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StoreHtmlInSession(string Html)
        {
            _httpContextAccessor.HttpContext.Session.SetString("PageHtml", Html);
            return Json(true);
        }

        public IActionResult PdfFromHtml()
        {
            var Html = _httpContextAccessor.HttpContext.Session.GetString("PageHtml");
            _httpContextAccessor.HttpContext.Session.Remove("PageHtml");
            return new ViewAsPdf("PdfFromHtml", Html)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}
