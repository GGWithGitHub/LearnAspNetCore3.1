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
    }
}
