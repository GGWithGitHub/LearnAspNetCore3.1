using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class PDFSampleController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PDFSampleController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostEnvironment)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
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
            var html = _httpContextAccessor.HttpContext.Session.GetString("PageHtml");

            _httpContextAccessor.HttpContext.Session.Remove("PageHtml");

            string relativeTocStyleFilePath = "xsl/tocStyle.xsl";
            // Construct the absolute file path
            string absoluteTocStyleFilePath = Path.Combine(_hostEnvironment.WebRootPath, relativeTocStyleFilePath);

            var relativeCoverPageFilePath = "html/coverPage.html";
            string absoluteCoverPageFilePath = Path.Combine(_hostEnvironment.WebRootPath, relativeCoverPageFilePath);

            //string customSwitches = "--footer-html " + Path.GetFullPath("~/Views/PDFSample/Footer.html").Replace("~\\", "");
            var viewPdf = new ViewAsPdf("PdfFromHtml",null, html)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                //PageMargins = new Rotativa.AspNetCore.Options.Margins(10, 20, 10, 20),
                CustomSwitches = $"--print-media-type --page-offset 0 --footer-center [page] cover {Url.Action("CoverPage", "PDFSample", null, Request.Scheme)} toc --toc-text-size-shrink 1"
                //CustomSwitches = $"--page-offset 0 --footer-center [page] toc --xsl-style-sheet {relativeTocStyleFilePath}"
                //CustomSwitches = $"--footer-html {Url.Action("TableOfContentsFooter", "PDFSample", null, Request.Scheme)}"
                //CustomSwitches = "--enable-local-file-access"
                //PageWidth = 200,
                //PageHeight = 100,
                //CustomSwitches = "--page-offset 0 --footer-center [page] --footer-font-size 8"
                //CustomSwitches = "--disable-smart-shrinking"
            };
            return viewPdf;
        }

        public IActionResult TableOfContentsFooter()
        {
            // Create a partial view with the dynamically generated table of contents
            return PartialView("_TableOfContents");
        }

        public IActionResult CoverPage()
        {
            return View();
        }

        public IActionResult TocStyle()
        {
            return View();
        }
    }
}
