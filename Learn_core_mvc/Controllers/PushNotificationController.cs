using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class PushNotificationController : Controller
    {
        public IActionResult SendPushNotification()
        {
            var sResponseFromServer = string.Empty;
            var fireBaseUrl = "https://fcm.googleapis.com/fcm/send";
            //var deviceToken = "eQnc_HuhQlee0labgXfgZC:APA91bGHnm44w7z1gSDksg4uQDJ09ZyYUbr3boZPP5hViqUjm35jiS-g_QZ8tGTCSuMIjCR-w0CzK7pWmD31l8WVAFR01EHQ3-rVHvtAleRdTHUw1PH4CFzZczmGjF7zxDl3vLNaScqo";
            var deviceToken = string.Empty;
            var serverAPIKey = string.Empty;
            var senderId = string.Empty;

            try
            {
                WebRequest tRequest = WebRequest.Create(fireBaseUrl);

                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                var data = new
                {
                    to = deviceToken,
                    priority = "high",
                    mutable_content = true,
                    content_available = true,
                    data = new
                    {
                        click_action = "SplashActivity"
                    },
                    notification = new
                    {
                        click_action = "SplashActivity"
                    },
                };

                string json = JsonSerializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverAPIKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // .NET 4.5
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // .NET 4.0

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                sResponseFromServer = tReader.ReadToEnd();

                                dynamic pushNotificationObject = null;
                                if (!string.IsNullOrEmpty(sResponseFromServer))
                                {
                                    pushNotificationObject = JsonSerializer.Deserialize<object>(sResponseFromServer);
                                }

                                string successMsg = "Notification send successfully";
                                if (pushNotificationObject?.success != 1)
                                {
                                    successMsg = "Error 91-Sending notification failed";
                                }

                                ViewBag.Msg = successMsg;

                                /*LogProvider.ManageExceptionLog<NotificationResponseViewModel>(
                                    "SendNotification Common Method sentBy:- " + methodNameCode
                                    , 0
                                    , deviceId
                                    , userId
                                    , null
                                    , sResponseFromServer
                                    , null
                                    , successCode
                                    , successMsg);

                                return new NotificationResponseViewModel
                                {
                                    success = pushNotificationObject?.success ?? 0,
                                    failure = pushNotificationObject?.failure ?? 0,
                                    jsonresults = sResponseFromServer,
                                    canonical_ids = pushNotificationObject?.canonical_ids ?? 0,
                                    ExceptonMessage = pushNotificationObject.results.FirstOrDefault()?.error
                                };*/

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                /*LogProvider.ManageExceptionLog<NotificationResponseViewModel>(
                    "SendNotification sentBy- " + methodNameCode
                    , 0
                    , deviceId
                    , userId
                    , ex.InnerException?.ToString()
                    , ex.Message
                    , null
                    , "0"
                    , "Error 91: Try again!");
                return new NotificationResponseViewModel
                {
                    failure = 1,
                    jsonresults = sResponseFromServer,
                    ExceptonMessage = ex.Message,
                };*/

                ViewBag.Msg = ex.Message;
            }

            return View();
        }
    
        public IActionResult NewSendPushNotification()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewSendPushNotification(object obj)
        {
            var credentialJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebase-service-account-file.json");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(credentialJsonFilePath),
            });

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = "",
                    Body = ""
                },
                Data = new Dictionary<string, string>
                {
                    { "custum", "Data Tag!" },
                    { "detail", "Data Message" },
                    { "title", "" },
                    { "body", "" },
                    { "sound", "" },
                    { "color", "" },
                    { "appeventtype", "" },
                    { "android_channel_id", "" },
                    { "click_action", "SplashActivity" },
                    { "mobileappmessage_id", "" }
                },
                Token = "",
                Android = new AndroidConfig() { Priority = Priority.High },
                Apns = new ApnsConfig() { Aps = new Aps { Sound = "" } }
            };

            try
            {
                // Send the message
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
    }
}
