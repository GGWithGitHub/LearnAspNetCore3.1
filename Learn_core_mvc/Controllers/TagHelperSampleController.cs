using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class TagHelperSampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AnchorTagRouteParam(string param1, int param2)
        {
            return RedirectToAction("Index");
        }

        public IActionResult AnchorTagAllRouteParam(string param1, int param2, bool param3)
        {
            return RedirectToAction("Index");
        }

        [Route("anchor-with-route-attr",Name = "ModifiedRouteName")]
        public IActionResult AnchorWithRouteAttribute()
        {
            return RedirectToAction("Index");
        }
    }
}
