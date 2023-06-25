using Learn_core_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.ViewModels
{
    public class OfficeJsJqVM
    {
        public List<EmployeeJsJqModel> Employees { get; set; } = new List<EmployeeJsJqModel>();
        public DepartmentJsJqModel Department { get; set; } = new DepartmentJsJqModel();
    }

    public class GamesJsJqVM
    {
        public List<CheckboxViewModel> Games { get; set; } = new List<CheckboxViewModel>();

        public bool ShowAsEditable { get; set; }
        public bool IsValidData { get; set; }

        public List<GamesJsJqModel> SelectedGames { get; set; }
        public List<GamesJsJqModel> GetGames()
        {
            var gameList = new List<GamesJsJqModel>() {
                new GamesJsJqModel() { Id = 1, Name = "Cricket" },
                new GamesJsJqModel() { Id = 2, Name = "Hockey" },
                new GamesJsJqModel() { Id = 3, Name = "Football" },
                new GamesJsJqModel() { Id = 4, Name = "Vollyball" },
                new GamesJsJqModel() { Id = 5, Name = "Chase" },
                new GamesJsJqModel() { Id = 6, Name = "Carrom" },
                new GamesJsJqModel() { Id = 7, Name = "Kabaddi" },
                new GamesJsJqModel() { Id = 8, Name = "Archery" },
                new GamesJsJqModel() { Id = 9, Name = "Table Tennis" },
                new GamesJsJqModel() { Id = 10, Name = "Badminton" },
            };
            return gameList;
        }
    }
}
