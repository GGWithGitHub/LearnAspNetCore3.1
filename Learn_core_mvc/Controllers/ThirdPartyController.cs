using ClosedXML.Excel;
using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Learn_core_mvc.Controllers
{
    public class ThirdPartyController : Controller
    {
        public IActionResult SendSmsByTwilio()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendSmsByTwilio(string toPhoneNumber, string phoneMsg)
        {
            var accountSID = "";
            var authToken = "";
            var fromPhoneNumber = "";

            TwilioClient.Init(accountSID, authToken);
            var messageOptions = new CreateMessageOptions(new PhoneNumber(toPhoneNumber));
            messageOptions.From = new PhoneNumber(fromPhoneNumber);
            messageOptions.Body = phoneMsg;
            try
            {
                var message = MessageResource.Create(messageOptions);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.InnerException?.Message ?? ex.Message;
            }

            return View();
        }

        public IActionResult ExcelIndex()
        {
            ExcelDataVM excelDataVM = new ExcelDataVM();
            ExcelDataModel excelDataModel = new ExcelDataModel();
            excelDataVM.ExcelDatas = excelDataModel.GetExcelDatas();
            
            return View(excelDataVM);
        }

        [HttpPost]
        public IActionResult ExcelExport()
        {
            ExcelDataModel excelDataModel = new ExcelDataModel();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6] {
                new DataColumn("Name"),
                new DataColumn("Email"),
                new DataColumn("Phone"),
                new DataColumn("City"),
                new DataColumn("State"),
                new DataColumn("Salary"),
            });
            var excelDatas = excelDataModel.GetExcelDatas();
            foreach (var excelData in excelDatas)
            {
                dt.Rows.Add(
                    excelData.Name,
                    excelData.Email,
                    excelData.Phone,
                    excelData.City,
                    excelData.State,
                    excelData.Salary
                );
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        public IActionResult ExportHtmlTblToExcelByJquery()
        {
            ExcelDataVM excelDataVM = new ExcelDataVM();
            ExcelDataModel excelDataModel = new ExcelDataModel();
            excelDataVM.ExcelDatas = excelDataModel.GetExcelDatas();

            return View(excelDataVM);
        }
        
        public IActionResult HtmlStringToExcelIndex()
        {
            ExcelDataVM excelDataVM = new ExcelDataVM();
            ExcelDataModel excelDataModel = new ExcelDataModel();
            excelDataVM.ExcelDatas = excelDataModel.GetExcelDatas();

            return View(excelDataVM);
        }

        [HttpPost]
        public FileResult ExportHtmlStringToExcel(string GridHtml)
        {
            return File(Encoding.ASCII.GetBytes(GridHtml), "application/vnd.ms-excel", "Grid.xls");
        }
    }
}
