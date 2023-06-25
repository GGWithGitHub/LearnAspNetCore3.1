using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class PartialViewSampleController : Controller
    {
        List<TeamModel> teamModels = new List<TeamModel>();
        public PartialViewSampleController()
        {
            teamModels.Add(new TeamModel() { Country="IND", PlayerId=1, PlayerName="Sachin", Player100Count=50 });
            teamModels.Add(new TeamModel() { Country="IND", PlayerId=2, PlayerName= "Sahwag", Player100Count = 40 });
            teamModels.Add(new TeamModel() { Country="IND", PlayerId=3, PlayerName= "Virat", Player100Count = 30 });
            teamModels.Add(new TeamModel() { Country="IND", PlayerId=4, PlayerName= "Dhoni", Player100Count = 20 });
            teamModels.Add(new TeamModel() { Country="IND", PlayerId=5, PlayerName= "Dravid", Player100Count = 25 });
            teamModels.Add(new TeamModel() { Country="AUS", PlayerId=6, PlayerName= "Riki", Player100Count = 45 });
            teamModels.Add(new TeamModel() { Country="AUS", PlayerId=7, PlayerName= "Andrew", Player100Count = 15 });
            teamModels.Add(new TeamModel() { Country="AUS", PlayerId=8, PlayerName= "Saymond", Player100Count = 22 });
            teamModels.Add(new TeamModel() { Country="AUS", PlayerId=9, PlayerName = "Gilcrist", Player100Count = 35 });
            teamModels.Add(new TeamModel() { Country="AUS", PlayerId=10, PlayerName = "David", Player100Count = 36 });
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PartialTagInView()
        {
            return View(teamModels);
        }

        public IActionResult ReturnPartialView()
        {
            return View();
        }

        public IActionResult PartialViewActionMethod(string shortCountryName)
        {
            var playersCountryWise = teamModels.Where(x => x.Country == shortCountryName).Select(x => x.PlayerName).ToList();
            return PartialView("_playerNamesPV", playersCountryWise);
        }

        public IActionResult PartialViewExample3()
        {
            return View(teamModels);
        }

        public IActionResult PartialViewExample4()
        {
            return View();
        }

        public IActionResult JqueryLoadPV(TestModel1 testModel1)
        {
            return PartialView("_jqueryLoadPV", testModel1);
        }
    }
}
