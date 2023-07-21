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
            return new ViewAsPdf("GeneratePDF");

            //var viewPDF = new ViewAsPdf("Index");
            //var pdfBytes = viewPDF.BuildFile(ControllerContext);
            //return viewPDF;
        }
    }
}
