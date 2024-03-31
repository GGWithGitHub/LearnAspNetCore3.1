using Learn_core_mvc.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class CallApiViaHttpClientController : Controller
    {
        private readonly string baseUrl = "https://dummy.restapiexample.com/api/v1/";
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Index2()
        {
            return View();
        }

        #region Call REST api using HttpClient example 1

        public IActionResult GetEmployees()
        {
            OutPutAPI<List<EmployeeHttpClient>> resultantObject = new OutPutAPI<List<EmployeeHttpClient>>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage APIResponse = new HttpResponseMessage();
                    var url = baseUrl + "employees";
                    APIResponse = httpClient.GetAsync(url).GetAwaiter().GetResult();
                    var resultEmpsJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    resultantObject = JsonConvert.DeserializeObject<OutPutAPI<List<EmployeeHttpClient>>>(resultEmpsJson);
                }
            }
            catch (Exception ex)
            {
                resultantObject.message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace;
            }
            return Json(resultantObject);
        }
        public IActionResult GetEmployee()
        {
            OutPutAPI<EmployeeHttpClient> resultantObject = new OutPutAPI<EmployeeHttpClient>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage APIResponse = new HttpResponseMessage();
                    var url = baseUrl + "employee/1";
                    APIResponse = httpClient.GetAsync(url).GetAwaiter().GetResult();
                    var resultEmpJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    resultantObject = JsonConvert.DeserializeObject<OutPutAPI<EmployeeHttpClient>>(resultEmpJson);
                }
            }
            catch (Exception ex)
            {
                resultantObject.message = ex.Message + Environment.NewLine + ex.InnerException.Message + Environment.NewLine + ex.StackTrace;
            }
            return Json(resultantObject);
        }
        public IActionResult CreateEmployee()
        {
            OutPutAPI<EmployeeHttpClient> resultantObject = new OutPutAPI<EmployeeHttpClient>();
            try
            {
                EmployeeHttpClient employeeObj = new EmployeeHttpClient()
                {
                    employee_name = "Bablu",
                    employee_age = 25,
                    employee_salary = 50000,
                    profile_image = ""
                };
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage APIResponse = new HttpResponseMessage();
                    if (ModelState.IsValid)
                    {
                        var empJson = JsonConvert.SerializeObject(employeeObj);
                        var stringContent = new StringContent(empJson, UnicodeEncoding.UTF8, "application/json");
                        var url = baseUrl + "create";
                        APIResponse = httpClient.PostAsync(url, stringContent).GetAwaiter().GetResult();
                        var resultEmpJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        resultantObject = JsonConvert.DeserializeObject<OutPutAPI<EmployeeHttpClient>>(resultEmpJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultantObject.message = ex.Message;
            }
            return Json(resultantObject);
        }
        public IActionResult UpdateEmployee()
        {
            OutPutAPI<EmployeeHttpClient> resultantObject = new OutPutAPI<EmployeeHttpClient>();
            try
            {
                EmployeeHttpClient employeeObj = new EmployeeHttpClient()
                {
                    employee_name = "Bablu",
                    employee_age = 25,
                    employee_salary = 50000,
                    profile_image = ""
                };
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage APIResponse = new HttpResponseMessage();
                    if (ModelState.IsValid)
                    {
                        var empJson = JsonConvert.SerializeObject(employeeObj);
                        var stringContent = new StringContent(empJson, UnicodeEncoding.UTF8, "application/json");
                        var url = baseUrl + "update/21";
                        APIResponse = httpClient.PutAsync(url, stringContent).GetAwaiter().GetResult();
                        var resultEmpJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        resultantObject = JsonConvert.DeserializeObject<OutPutAPI<EmployeeHttpClient>>(resultEmpJson);
                    }
                }
            }
            catch (Exception ex)
            {
                resultantObject.message = ex.Message;
            }
            return Json(resultantObject);
        }
        public ActionResult DeleteEmployee()
        {
            OutPutAPI<string> resultantObject = new OutPutAPI<string>();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage APIResponse = new HttpResponseMessage();
                    var url = baseUrl + "delete/2";
                    APIResponse = httpClient.DeleteAsync(url).GetAwaiter().GetResult();
                    var resultEmpJson = APIResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    resultantObject = JsonConvert.DeserializeObject<OutPutAPI<string>>(resultEmpJson);
                }
            }
            catch (Exception ex)
            {
                resultantObject.message = ex.Message;
            }
            return Json(resultantObject);
        }

        #endregion

        #region Call REST api using HttpClient example 2

        public PostMethodResponse Request(RequestMethod method, string url, object data=null, string reqHeader=null)
        {
            PostMethodResponse _resMsg = new PostMethodResponse();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                if (!string.IsNullOrEmpty(reqHeader))
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization",reqHeader);
                }
                var myContent = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(myContent, Encoding.UTF8, "application/json");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                HttpResponseMessage res = new HttpResponseMessage();
                switch (method)
                {
                    case RequestMethod.Post:
                        res = client.PostAsync(url, httpContent).GetAwaiter().GetResult();
                        break;

                    case RequestMethod.Get:
                        res = client.GetAsync(url).GetAwaiter().GetResult();
                        break;

                    case RequestMethod.Put:
                        res = client.PutAsync(url, httpContent).GetAwaiter().GetResult();
                        break;

                    case RequestMethod.Delete:
                        res = client.DeleteAsync(url).GetAwaiter().GetResult();
                        break;
                }
                
                if (res.IsSuccessStatusCode)
                {
                    _resMsg.MessageType = MessageType.Success.ToString();
                }
                else
                {
                    _resMsg.MessageType = MessageType.Fail.ToString();
                }
                _resMsg.Content = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }
            return _resMsg;
        }

        public IActionResult SendResponse<T>(MessageType messageType, string message=default, T data=default, Exception exception=default)
        {
            OutPutAPI<T> outPut = new OutPutAPI<T>();
            if (messageType == MessageType.Fail)
            {
                outPut.status = MessageType.Fail.ToString();
                outPut.message = message;
                return Json(outPut);
            }
            if (messageType == MessageType.Error)
            {
                outPut.status = MessageType.Error.ToString();
                outPut.message = exception.Message + Environment.NewLine + exception.InnerException?.Message + Environment.NewLine + exception.StackTrace;
                return Json(outPut);
            }
            outPut.status = MessageType.Success.ToString();
            outPut.message = message;
            outPut.data = data;
            return Json(outPut);
        }

        public IActionResult GetEmployees2()
        {
            OutPutAPI<List<EmployeeHttpClient>> resultantObject = new OutPutAPI<List<EmployeeHttpClient>>();
            try
            {
                var url = baseUrl + "employees";
                var res = Request(RequestMethod.Get, url);
                if (res.MessageType == MessageType.Fail.ToString())
                {
                    return SendResponse<object>(MessageType.Fail, res.Content);
                }
                var resultEmpsJson = res.Content;
                resultantObject = JsonConvert.DeserializeObject<OutPutAPI<List<EmployeeHttpClient>>>(resultEmpsJson);
                return SendResponse<List<EmployeeHttpClient>>(MessageType.Success, resultantObject.message, resultantObject.data);
            }
            catch (Exception ex)
            {
                return SendResponse<object>(messageType:MessageType.Error, exception:ex);
            }
        }
        public IActionResult GetEmployee2()
        {
            OutPutAPI<EmployeeHttpClient> resultantObject = new OutPutAPI<EmployeeHttpClient>();
            try
            {
                var url = baseUrl + "employee/1";
                var res = Request(RequestMethod.Get, url);
                if (res.MessageType == MessageType.Fail.ToString())
                {
                    return SendResponse<object>(MessageType.Fail, res.Content);
                }
                var resultEmpJson = res.Content;
                resultantObject = JsonConvert.DeserializeObject<OutPutAPI<EmployeeHttpClient>>(resultEmpJson);
                return SendResponse<EmployeeHttpClient>(MessageType.Success, resultantObject.message, resultantObject.data);
            }
            catch (Exception ex)
            {
                return SendResponse<object>(messageType: MessageType.Error, exception: ex);
            }
        }
        public IActionResult CreateEmployee2()
        {
            OutPutAPI<EmployeeHttpClient> resultantObject = new OutPutAPI<EmployeeHttpClient>();
            try
            {
                EmployeeHttpClient employeeObj = new EmployeeHttpClient()
                {
                    employee_name = "Bablu",
                    employee_age = 25,
                    employee_salary = 50000,
                    profile_image = ""
                };
                if (!ModelState.IsValid)
                {
                    var failMsg = "Model is not valid";
                    return SendResponse<object>(MessageType.Fail, failMsg);
                }
                var url = baseUrl + "create";
                var res = Request(RequestMethod.Post, url, employeeObj);
                if (res.MessageType == MessageType.Fail.ToString())
                {
                    return SendResponse<object>(MessageType.Fail, res.Content);
                }
                var resultEmpJson = res.Content;
                resultantObject = JsonConvert.DeserializeObject<OutPutAPI<EmployeeHttpClient>>(resultEmpJson);
                return SendResponse<EmployeeHttpClient>(MessageType.Success, resultantObject.message, resultantObject.data);
            }
            catch (Exception ex)
            {
                return SendResponse<object>(messageType: MessageType.Error, exception: ex);
            }
        }
        public IActionResult UpdateEmployee2()
        {
            OutPutAPI<EmployeeHttpClient> resultantObject = new OutPutAPI<EmployeeHttpClient>();
            try
            {
                EmployeeHttpClient employeeObj = new EmployeeHttpClient()
                {
                    employee_name = "Bablu",
                    employee_age = 25,
                    employee_salary = 50000,
                    profile_image = ""
                };
                if (!ModelState.IsValid)
                {
                    var failMsg = "Model is not valid";
                    return SendResponse<object>(MessageType.Fail, failMsg);
                }
                var url = baseUrl + "update/21";
                var res = Request(RequestMethod.Put, url, employeeObj);
                if (res.MessageType == MessageType.Fail.ToString())
                {
                    return SendResponse<object>(MessageType.Fail, res.Content);
                }
                var resultEmpJson = res.Content;
                resultantObject = JsonConvert.DeserializeObject<OutPutAPI<EmployeeHttpClient>>(resultEmpJson);
                return SendResponse<EmployeeHttpClient>(MessageType.Success, resultantObject.message, resultantObject.data);
            }
            catch (Exception ex)
            {
                return SendResponse<object>(messageType: MessageType.Error, exception: ex);
            }
        }
        public IActionResult DeleteEmployee2()
        {
            OutPutAPI<string> resultantObject = new OutPutAPI<string>();
            try
            {
                var url = baseUrl + "delete/2";
                var res = Request(RequestMethod.Delete, url);
                if (res.MessageType == MessageType.Fail.ToString())
                {
                    return SendResponse<object>(MessageType.Fail, res.Content);
                }
                var resultEmpJson = res.Content;
                resultantObject = JsonConvert.DeserializeObject<OutPutAPI<string>>(resultEmpJson);
                return SendResponse<string>(MessageType.Success, resultantObject.message, resultantObject.data);
            }
            catch (Exception ex)
            {
                return SendResponse<object>(messageType: MessageType.Error, exception: ex);
            }
        }

        #endregion
    }

    public class PostMethodResponse
    {
        public string MessageType { get; set; }
        public string Content { get; set; }
    }

    public enum RequestMethod
    {
        Get,
        Post,
        Put,
        Delete
    }

    public enum MessageType
    {
        Success,
        Fail,
        Error
    }
}
