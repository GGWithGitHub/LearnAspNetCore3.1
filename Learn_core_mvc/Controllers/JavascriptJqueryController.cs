using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class JavascriptJqueryController : Controller
    {
        private static List<GamesJsJqModel> GamesJsJqStatic = new List<GamesJsJqModel>();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormValidation()
        {
            return View();
        }
        public IActionResult ShowLoading()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowLoadingPost()
        {
            return View("ShowLoading");
        }
        
        public IActionResult PostListObject()
        {
            OfficeJsJqVM officeJsJqVM = new OfficeJsJqVM();

            officeJsJqVM.Employees.Add(new EmployeeJsJqModel { Id=1, Name= "Gaurav", Email= "gaurv@gmail.com" });
            officeJsJqVM.Employees.Add(new EmployeeJsJqModel { Id=2, Name= "Aurav", Email= "aaurv@gmail.com" });
            officeJsJqVM.Employees.Add(new EmployeeJsJqModel { Id=3, Name= "Daurav", Email= "daurv@gmail.com" });
            officeJsJqVM.Department = new DepartmentJsJqModel()
            {
                Id = 100,
                Name = "IT"
            };

            return View(officeJsJqVM);
        }

        [HttpPost]
        public IActionResult PostListObject([FromBody]OfficeJsJqVM officeJsJqVM)
        {
            return View(officeJsJqVM);
        }

        public IActionResult SaveAndReadOnly(bool showInEdit=false)
        {
            GamesJsJqVM gamesJsJqVM = new GamesJsJqVM();

            var gameList = gamesJsJqVM.GetGames();
            foreach (var game in gameList)
            {
                var chkModel = new CheckboxViewModel { Id = game.Id, LabelName = game.Name, IsChecked = false };
                gamesJsJqVM.Games.Add(chkModel);
            }

            if (GamesJsJqStatic.Count>0)
            {
                foreach (var game in GamesJsJqStatic)
                {
                    gamesJsJqVM.Games.Where(x => x.Id == game.Id).FirstOrDefault().IsChecked = true;
                }
                gamesJsJqVM.SelectedGames = GamesJsJqStatic;

                gamesJsJqVM.IsValidData = true;
            }

            gamesJsJqVM.ShowAsEditable = showInEdit;
            return View(gamesJsJqVM);
        }

        [HttpPost]
        public IActionResult SaveAndReadOnly(GamesJsJqVM gamesJsJqVM)
        {
            var isAnyGameSelected = gamesJsJqVM.Games.Any(x => x.IsChecked == true);
            if (isAnyGameSelected)
            {
                var selectedGames = gamesJsJqVM.Games.Where(x=>x.IsChecked == true).ToList();
                GamesJsJqStatic.Clear();
                foreach (var game in selectedGames)
                {
                    var gameJsJsModel = new GamesJsJqModel() { Id = game.Id, Name = game.LabelName };

                    GamesJsJqStatic.Add(gameJsJsModel);
                }
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status=false });
            }
        }

        public IActionResult WaitFunction()
        {
            return View();
        }
        public IActionResult WaitFun1()
        {
            Thread.Sleep(1000);
            return Json(new { status=true,data="function 1 result" });
        }
        public IActionResult WaitFun2()
        {
            return Json(new { status = true, data = "function 2 result" });
        }

        public IActionResult ShowMap()
        {
            return View();
        }

        public IActionResult SearchDropdown()
        {
            SearchDrpDwnModel searchDrpDwnModel = new SearchDrpDwnModel();
            return View(searchDrpDwnModel);
        }

        public IActionResult GetModelList()
        {
            StudentJsJqVM studentJsJqVM = new StudentJsJqVM() { Students = new List<StudentJsJqModel>() };
            studentJsJqVM.Students.Add(new StudentJsJqModel() { Id = 1,Name = "Golu",Email = "golu@gmail.com",Phone = "9854785698"});
            studentJsJqVM.Students.Add(new StudentJsJqModel(){Id = 2,Name = "Molu",Email = "molu@gmail.com",Phone = "8854785698"});
            return View(studentJsJqVM);
        }

        public IActionResult DotInName()
        {
            DotInNameModel model = new DotInNameModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult DotInName(DotInNameModel model)
        {
            return View(model);
        }

        public IActionResult WarnUserBeforeLeavingPage()
        {
            return View();
        }

        public IActionResult Cascading()
        {
            var countries = new List<CascadCountryModel>
            {
                new CascadCountryModel{ CountryId="IND", CountryName="India"},
                new CascadCountryModel{ CountryId="FR", CountryName="France"},
                new CascadCountryModel{ CountryId="AUS", CountryName="Australia"},
                new CascadCountryModel{ CountryId="BRZ", CountryName="Brazil"},
                new CascadCountryModel{ CountryId="CND", CountryName="Canada"}
            };
            ViewBag.Countries = countries.ToList();
            return View();
        }

        [HttpGet]
        public JsonResult GetStates(string countryId)
        {
            var statesList = new List<CascadStateModel>
            {
                new CascadStateModel{ StateId="AP", StateName="Andhra Pradesh", CountryId="IND" },
                new CascadStateModel{ StateId="JHA", StateName="Jharkhand", CountryId="IND" },
                new CascadStateModel{ StateId="GUJ", StateName="Gujarat", CountryId="IND" },

                new CascadStateModel{ StateId="BRI", StateName="Brittany", CountryId="FR" },
                new CascadStateModel{ StateId="COR", StateName="Corsica", CountryId="FR" },
                new CascadStateModel{ StateId="ALS", StateName="Alsace", CountryId="FR" },

                new CascadStateModel{ StateId="QLD", StateName="Queensland", CountryId="AUS" },
                new CascadStateModel{ StateId="TAS", StateName="Tasmania", CountryId="AUS" },
                new CascadStateModel{ StateId="VIC", StateName="Victoria", CountryId="AUS" },

                new CascadStateModel{ StateId="AM", StateName="Amazonas", CountryId="BRZ" },
                new CascadStateModel{ StateId="AC", StateName="Acre", CountryId="BRZ" },
                new CascadStateModel{ StateId="TO", StateName="Tocantins", CountryId="BRZ" },

                new CascadStateModel{ StateId="AB", StateName="Alberta", CountryId="CND" },
                new CascadStateModel{ StateId="MB", StateName="Manitoba", CountryId="CND" },
                new CascadStateModel{ StateId="YT", StateName="Yukon", CountryId="CND" }
            };

            var states = statesList.Where(s => s.CountryId == countryId).ToList();
            return Json(states);
        }

        [HttpGet]
        public JsonResult GetCities(string stateId)
        {
            var citiesList = new List<CascadCityModel>
            {
                new CascadCityModel{ StateId="AP",  CityName="Visakhapatnam", CityId="VIS" },
                new CascadCityModel{ StateId="AP",  CityName="Guntur", CityId="GUN" },
                new CascadCityModel{ StateId="AP",  CityName="Kurnool", CityId="KUR" },

                new CascadCityModel{ StateId="JHA", CityName="Jamshedpur", CityId="JAM" },
                new CascadCityModel{ StateId="JHA", CityName="Ranchi", CityId="RAN" },
                new CascadCityModel{ StateId="JHA", CityName="Dhanbad", CityId="DHN" },

                new CascadCityModel{ StateId="GUJ", CityName="", CityId="" },
                new CascadCityModel{ StateId="GUJ", CityName="", CityId="" },
                new CascadCityModel{ StateId="GUJ", CityName="", CityId="" },

                new CascadCityModel{ StateId="BRI", CityName="", CityId="" },
                new CascadCityModel{ StateId="BRI", CityName="", CityId="" },
                new CascadCityModel{ StateId="BRI", CityName="", CityId="" },

                new CascadCityModel{ StateId="COR", CityName="", CityId="" },
                new CascadCityModel{ StateId="COR", CityName="", CityId="" },
                new CascadCityModel{ StateId="COR", CityName="", CityId="" },

                new CascadCityModel{ StateId="ALS", CityName="", CityId="" },
                new CascadCityModel{ StateId="ALS", CityName="", CityId="" },
                new CascadCityModel{ StateId="ALS", CityName="", CityId="" },

                new CascadCityModel{ StateId="QLD", CityName="", CityId="" },
                new CascadCityModel{ StateId="QLD", CityName="", CityId="" },
                new CascadCityModel{ StateId="QLD", CityName="", CityId="" },

                new CascadCityModel{ StateId="TAS", CityName="", CityId="" },
                new CascadCityModel{ StateId="TAS", CityName="", CityId="" },
                new CascadCityModel{ StateId="TAS", CityName="", CityId="" },

                new CascadCityModel{ StateId="VIC", CityName="", CityId="" },
                new CascadCityModel{ StateId="VIC", CityName="", CityId="" },
                new CascadCityModel{ StateId="VIC", CityName="", CityId="" },

                new CascadCityModel{ StateId="AM",  CityName="", CityId="" },
                new CascadCityModel{ StateId="AM",  CityName="", CityId="" },
                new CascadCityModel{ StateId="AM",  CityName="", CityId="" },

                new CascadCityModel{ StateId="AC",  CityName="", CityId="" },
                new CascadCityModel{ StateId="AC",  CityName="", CityId="" },
                new CascadCityModel{ StateId="AC",  CityName="", CityId="" },

                new CascadCityModel{ StateId="TO",  CityName="", CityId="" },
                new CascadCityModel{ StateId="TO",  CityName="", CityId="" },
                new CascadCityModel{ StateId="TO",  CityName="", CityId="" },

                new CascadCityModel{ StateId="AB",  CityName="", CityId="" },
                new CascadCityModel{ StateId="AB",  CityName="", CityId="" },
                new CascadCityModel{ StateId="AB",  CityName="", CityId="" },

                new CascadCityModel{ StateId="MB",  CityName="", CityId="" },
                new CascadCityModel{ StateId="MB",  CityName="", CityId="" },
                new CascadCityModel{ StateId="MB",  CityName="", CityId="" },

                new CascadCityModel{ StateId="YT",  CityName="", CityId="" },
                new CascadCityModel{ StateId="YT",  CityName="", CityId="" },
                new CascadCityModel{ StateId="YT",  CityName="", CityId="" }
            };
            var cities = citiesList.Where(c => c.StateId == stateId).ToList();
            return Json(cities);
        }
    }
}
