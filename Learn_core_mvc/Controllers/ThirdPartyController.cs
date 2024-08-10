using ClosedXML.Excel;
using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinCan;
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

        public IActionResult ResFromChatGpt()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UseChatGPT(ChatGptReqModel model)
        {
            string chatApiKey = "";  
            var prompt = "For this data: ";
            string combinedText = $"{prompt} {model.ReqContent}";

            var requestBody = new
            {
                model = "gpt-3.5-turbo-0125",
                messages = new[] {
                new { role = "system", content = prompt },
                new { role = "user", content = combinedText }
            },
                max_tokens = 1000,
                temperature = 0.1
            };

            var jsonPayload = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var chatGptUrl = "https://api.openai.com/v1/chat/completions";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", chatApiKey);

                try
                {
                    HttpResponseMessage response = await client.PostAsync(chatGptUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, message = responseContent });
                    }
                    else
                    {
                        return Json(new { success = false, message = responseContent });
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details here to understand what went wrong
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        public IActionResult MqttExample()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MqttExample(string data,string mqttBrokerIp, int mqttBrokerPort, 
            string mqttBrokerUsername, string mqttBrokerPassword)
        {
            // Specify the MQTT broker options using the dynamic values
            var factory = new MqttFactory();
            var client = factory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(mqttBrokerIp, mqttBrokerPort)
                .WithCredentials(mqttBrokerUsername, mqttBrokerPassword)
                .Build();

            try
            {
                // Connect to the MQTT broker
                await client.ConnectAsync(options, CancellationToken.None);

                string jsonPayload = data;
                string mqttTopic = "xapi/statements/";

                // Create an MQTT application message with the JSON payload
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic(mqttTopic)
                    .WithPayload(jsonPayload)
                    .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                    .WithRetainFlag(false)
                    .Build();

                // Publish the message to the MQTT broker
                await client.PublishAsync(message, CancellationToken.None);

                ViewBag.msg = "Data send successfully ...";
            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.InnerException?.Message ?? ex.Message;
            }
            finally
            {
                // Disconnect from the MQTT broker and re-enable the button
                if (client.IsConnected)
                    await client.DisconnectAsync();
            }
            return View();
        }

        public IActionResult Lrs()
        {
            return View();
        }
        
        public IActionResult GetRegStatements(string registrationId)
        {
            string endpoint = "https://www.lrsendpoint.com/xapihelloworld/xAPI/";
            string username = "komufe";
            string password = "amcopg";

            try
            {
                var lrs = new RemoteLRS(endpoint, username, password);
                
                var query = new StatementsQuery();
                query.limit = 5000;

                var allStatements = new List<Statement>();
                var more = true;
                while (more)
                {
                    var statementsResult = lrs.QueryStatements(query);
                    List<Statement> statements = statementsResult.content.statements;

                    allStatements.AddRange(statements);

                    more = statementsResult.content.more != null;

                    if (more)
                    {
                        query.until = statements[statements.Count - 1].timestamp.Value;
                    }
                }

                if (allStatements.Any())
                {
                    var filteredStatements = allStatements.Where(statement =>
                    statement.context != null &&
                    statement.context.registration != null &&
                    statement.context.registration.ToString() == registrationId).ToList();

                    if (filteredStatements.Any())
                    {
                        var statementsOfReg = JsonConvert.SerializeObject(filteredStatements);

                        return Json(new { success = true, data = statementsOfReg });
                    }
                    else
                    {
                        return Json(new { success = false, msg = "Can not get the registration's data." });
                    }
                }
                else
                {
                    return Json(new { success = false, msg = "Can not get the data." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Connection failed" });
            }
        }

        public IActionResult LrsJs()
        {
            return View();
        }
    }
}
