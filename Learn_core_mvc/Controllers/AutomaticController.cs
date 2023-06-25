using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class AutomaticController : Controller
    {
        public int counter = 0;

        public AutomaticController()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }
        public IActionResult Index()
        {
            return View();
        }

        public void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
        }
    }
}
