using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
