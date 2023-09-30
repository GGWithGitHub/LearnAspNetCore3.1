﻿using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Models;
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

        public async Task<IActionResult> ServerSideDT()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetServerSideDTData(ServerSideDataTableRequest request)
        {
            ServerSideDtModel model = new ServerSideDtModel();
            var data = model.GetData();

            // Apply searching, sorting, and paging
            if (!string.IsNullOrEmpty(request.Search?.Value))
            {
                data = data.Where(p =>
                    p.EmpName.Contains(request.Search.Value, StringComparison.OrdinalIgnoreCase) ||
                    p.EmpEmail.Contains(request.Search.Value, StringComparison.OrdinalIgnoreCase) ||
                    p.EmpAddress.Contains(request.Search.Value, StringComparison.OrdinalIgnoreCase) ||
                    p.EmpSalary.ToString().Contains(request.Search.Value, StringComparison.OrdinalIgnoreCase) ||
                    p.EmpJoinDate.ToString().Contains(request.Search.Value, StringComparison.OrdinalIgnoreCase) 
                ).ToList();
            }

            if (request.Order != null && request.Order.Any())
            {
                var orderColumn = request.Order[0].Column;
                var orderDir = request.Order[0].Dir;

                switch (orderColumn)
                {
                    case 0:
                        data = orderDir == "asc" ? data.OrderBy(p => p.EmpName).ToList() : data.OrderByDescending(p => p.EmpName).ToList();
                        break;
                    case 1:
                        data = orderDir == "asc" ? data.OrderBy(p => p.EmpEmail).ToList() : data.OrderByDescending(p => p.EmpEmail).ToList();
                        break;
                    case 2:
                        data = orderDir == "asc" ? data.OrderBy(p => p.EmpAddress).ToList() : data.OrderByDescending(p => p.EmpAddress).ToList();
                        break;
                    case 3:
                        data = orderDir == "asc" ? data.OrderBy(p => p.EmpSalary).ToList() : data.OrderByDescending(p => p.EmpSalary).ToList();
                        break;
                    case 4:
                        data = orderDir == "asc" ? data.OrderBy(p => p.EmpJoinDate).ToList() : data.OrderByDescending(p => p.EmpJoinDate).ToList();
                        break;
                }
            }

            var totalRecords = data.Count;
            var filteredData = data.Skip(request.Start).Take(request.Length);

            return Json(new
            {
                draw = request.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data = filteredData
            });
        }
    }
}
