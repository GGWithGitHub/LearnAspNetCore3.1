using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Learn_core_mvc.Controllers
{
    public class AIController : Controller
    {
        private readonly HttpClient _httpClient;

        public AIController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CallGrok()
        {
            var simplifyResponse = await AskGrok("");
            return Json(new { message = "xAI (Grok) called successfully." });
        }

        [HttpPost]
        public async Task<IActionResult> CallChatGPT()
        {
            var simplifyResponse = await AskChatGpt("");
            return Json(new { message = "ChatGPT called successfully." });
        }

        private async Task<string> AskGrok(string prompt)
        {
            string apiKey = "";
            var requestBody = new
            {
                model = "grok-2-latest",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 1000,
                temperature = 0.7
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.x.ai/v1/chat/completions"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var response = await _httpClient.SendAsync(httpRequest);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var parsed = JsonConvert.DeserializeObject<dynamic>(responseText);
            string reply = parsed?.choices?[0]?.message?.content?.ToString();

            return reply ?? "No response received.";
        }

        private async Task<string> AskChatGpt(string prompt)
        {
            string apiKey = "";

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.openai.com/v1/chat/completions"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.SendAsync(httpRequest);
            var responseText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var parsed = JsonConvert.DeserializeObject<dynamic>(responseText);
            string reply = parsed?.choices?[0]?.message?.content?.ToString();

            return reply ?? "No response received.";
        }
    }
}
