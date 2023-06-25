using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class DataTableController : Controller
    {
        ISampleService sampleService;
        public DataTableController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            DatatableProperties dtProp = new DatatableProperties();
            dtProp.Draw = Convert.ToInt32(Request.Form["draw"].FirstOrDefault());
            dtProp.Row = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            dtProp.RowPerPage = Convert.ToInt32(Request.Form["length"].FirstOrDefault()); // Rows display per page
            dtProp.ColumnIndex = Convert.ToInt32(Request.Form["order[0][column]"].FirstOrDefault()); // Column index
            int columnIndex = Convert.ToInt32(dtProp.ColumnIndex);
            dtProp.ColName = Request.Form["columns["+ columnIndex + "][data]"].FirstOrDefault(); // Column name
            dtProp.ColumnSortOrder = Request.Form["order[0][dir]"].FirstOrDefault(); // asc or desc
            dtProp.SearchValue = Request.Form["search[value]"].FirstOrDefault(); // Search value

            var res = await this.sampleService.GetDataTableEmps(dtProp);

            var d = new {
                draw = dtProp.Draw,
                recordsTotal = res.TotalRecords,
                recordsFiltered = res.TotalRecordWithFilter,
                data = res.Data
            };

            return Json(d);
        }
    }
}
