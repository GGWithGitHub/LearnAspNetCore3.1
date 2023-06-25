using Learn_core_mvc.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    }
}
