﻿using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Configuration;

namespace Learn_core_mvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        public PaymentController(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("Stripe:Secret");
        }
        public IActionResult StripeCheckout()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CheckEmailExists(string email)
        {
            if (email == "abc@gmail.com" || email == "xyz@gmail.com")
            {
                return Json(false);
            }
            return Json(true);
        }

        public IActionResult CheckStripeUserForm(StripeUserFormModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = true });
            }
            else
            {
                // Collect all error messages from ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                return Json(new { success = false, errors });
            }
        }

        [HttpPost]
        public ActionResult CreateCheckoutSession(
            string firstName,
            string lastName,
            string email,
            string password,
            string confirmPassword,
            string smsPhone,
            string address1,
            string city,
            string state,
            string country,
            int subscriptionTypeId
        )
        {
            try
            {
                var customParameters = new Dictionary<string, string>
            {
                { "firstName", firstName },
                { "lastName", lastName },
                { "email", email },
                { "password", password },
                { "smsPhone", smsPhone },
                { "address1", address1 },
                { "city", city },
                { "state", state },
                { "country", country },
                { "subscriptionTypeId", subscriptionTypeId.ToString() }
            };

                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    //BillingAddressCollection = "required",
                    CustomerEmail = email,
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Product name",
                                },
                                UnitAmount = 10 * 100, // amount in cents
							},
                            Quantity = 1,
                        },
                    },
                    Metadata = customParameters, // Add custom parameters to metadata
                    Mode = "payment",
                    SuccessUrl = baseUrl + "/Payment/StripeSuccess?sessionId={CHECKOUT_SESSION_ID}",
                    CancelUrl = $"{baseUrl}/Payment/StripeCancel",
                };

                var service = new SessionService();
                var session = service.Create(options);

                options.SuccessUrl = options.SuccessUrl.Replace("{CHECKOUT_SESSION_ID}", session.Id);

                return Json(new { success = true, sessionId = session.Id });
            }
            catch (Exception ex)
            {
                var error = ex.Message ?? ex.InnerException.Message;
                return Json(new { success = false, error });
            }
        }

        [HttpGet]
        public ActionResult StripeSuccess(string sessionId)
        {
            var service = new SessionService();
            var session = service.Get(sessionId);

            var firstName = session.Metadata["firstName"];
            var lastName = session.Metadata["lastName"];
            var email = session.Metadata["email"];
            var password = session.Metadata["password"];
            var smsPhone = session.Metadata["smsPhone"];
            var address1 = session.Metadata["address1"];
            var city = session.Metadata["city"];
            var state = session.Metadata["state"];
            var country = session.Metadata["country"];
            var subscriptionTypeId = session.Metadata["subscriptionTypeId"];

            return View();
        }

        [HttpGet]
        public ActionResult StripeCancel()
        {
            return View();
        }

        public IActionResult DotnetCharge()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DotnetCharge(object obj)
        {
            var objcc = new dotnetCHARGE.CC();

            objcc.Amount = default; // doubt in this 
            objcc.Number = default;//TxtCardNumber.Text.ToString;
            objcc.Month = default;//Convert.ToInt32(TxtExpiredMonth.Text);
            objcc.Year = default;  //Convert.ToInt32(TxtExpiredYear.Text);
            objcc.AvsCode = default;
            // objcc.TransactionKey = "7h5p43z823yPDKr9"
            objcc.Login = default;
            objcc.UserName = default;
            objcc.Password = default;
            objcc.MerchantUrl = default;
            objcc.Server = default;
            objcc.TransactionType = default;
            objcc.AvsIndicator = default; //0: no avs 1:full avs 2:only full avs 3:avs zip 4:only avs zip
            objcc.FirstName = default;      //txtBillingFirstName.Text.Trim;
            objcc.LastName = default;        //txtBillingLastName.Text.Trim;
            objcc.Address = default;         //txtBillingAddress1.Text.Trim;
            objcc.City = default;                //txtBillingCity.Text.Trim;
            objcc.StateProvince = default;      //txtBillingState.Text.Trim;
            objcc.ZipPostal = default;            //txtBillingZipCode.Text.Trim;
            objcc.Country = default;          //DropDownListBillingCountry.SelectedValue.Trim;
            objcc.Phone = default;
            objcc.TestMode = default;

            int iResult = objcc.Charge(dotnetCHARGE.Processor.ElavonConverge);  // execute the charge

            return View();
        }
    }
}
