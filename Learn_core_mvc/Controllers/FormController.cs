﻿using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class FormController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            FormViewModel formViewModel = new FormViewModel();
            return View(formViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(FormViewModel formViewModel)
        {
            if (ModelState.IsValid)
            {

            }

            var userName = formViewModel.UserName;
            var userPass = formViewModel.UserPassword;
            var userCountry = formViewModel.UserCountries.Where(x => x.Value == formViewModel.UserCountry).FirstOrDefault().Value;
            var userGender = formViewModel.UserGender;
            var isTermsAccepted = formViewModel.AcceptsTerms;
            var userSubjects = formViewModel.UserSubjects.Where(x => x.IsChecked == true).ToList();

            ViewBag.msg = "Form Submitted";
            ModelState.Clear();
            FormViewModel frm = new FormViewModel();
            return View(frm);
        }

        public IActionResult FormFill()
        {
            FormViewModel formViewModel = new FormViewModel();
            formViewModel.UserName = "Aman";
            formViewModel.UserPassword = "123";
            formViewModel.UserGender = "M";
            formViewModel.UserCountry = "FR";
            formViewModel.AcceptsTerms = true;
            formViewModel.UserSubjects = new List<CheckboxViewModel>
            {
                new CheckboxViewModel { Id = 1, LabelName = "Physics", IsChecked = true },
                new CheckboxViewModel { Id = 2, LabelName = "Chemistry", IsChecked = false },
                new CheckboxViewModel { Id = 3, LabelName = "Mathematics", IsChecked = true },
                new CheckboxViewModel { Id = 4, LabelName = "Biology", IsChecked = false },
                new CheckboxViewModel { Id = 5, LabelName = "Commerce", IsChecked = true }
            };

            return View("Index", formViewModel);
        }

        public IActionResult CheckEmailExist(string email)
        {
            if (email == "abc@gmail.com")
            {
                return Json(true);
            }
            else
            {
                return Json("Email address already exist. Please try another one");
            }
        }

        public IActionResult FormValidation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FormValidation(FormValModel frm)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        private void ValidateRecaptchaResponse(string recaptchaResponse)
        {
            if (!String.IsNullOrWhiteSpace(recaptchaResponse))
            {
                var client = new System.Net.WebClient();

                string privateKey = "6LeT3swUAAAAAN1nNtGWWLpEPWrhXWuTVPw6mN3o";

                var googleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", privateKey, recaptchaResponse));

                var reCaptchaVerificationResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ReCaptchaVerificationResponse>(googleReply);

                if (reCaptchaVerificationResponse == null || !reCaptchaVerificationResponse.Success)
                {
                    this.ModelState.AddModelError("RecaptchaResponse", "Please prove that you are a human.");
                }
            }
        }

        public IActionResult FormValidationWithCaptcha()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FormValidationWithCaptcha(CaptchaFormValModel frm)
        {
            this.ValidateRecaptchaResponse(frm.RecaptchaResponse);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors))
                {
                    ViewBag.Error += $"{error.ErrorMessage} <br>";
                }
            }
            return View();
        }

        public IActionResult FormValidationGoogleReCaptureV3()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FormValidationGoogleReCaptureV3(ContactUsVM model)
        {
            if (ModelState.IsValid)
            {
                var isCaptchaValid = await IsCaptchaValid(model.GoogleCaptchaToken);
                if (isCaptchaValid)
                {
                    ViewBag.Success = "<h4>Form submitted successfully.</h4>";
                }
                else
                {
                    ModelState.AddModelError("GoogleCaptcha", "The captcha is not valid");
                }
            }
            return View(model);
        }

        private async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                var secret = "6LcHpYoUAAAAAIQTMx-RL3WjsTN9k710FPn-DpOw";
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                    {
                        {"secret", secret},
                        {"response", response},
                        {"remoteip", Request.Host.Value}
                    };

                    var content = new FormUrlEncodedContent(values);
                    var verify = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
                    var captchaResponseJson = await verify.Content.ReadAsStringAsync();
                    var captchaResult = JsonConvert.DeserializeObject<CaptchaResponseVM>(captchaResponseJson);
                    return captchaResult.Success
                           && captchaResult.Action == "contact_us"
                           && captchaResult.Score > 0.5;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public IActionResult FormWithDynmcDdRbCb()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FormWithDynmcDdRbCb(FormWithDynmcDdRbCbModel frm)
        {
            return View();
        }

        public IActionResult FormSubmitJqueryAjax()
        {
            FormViewModel frm = new FormViewModel();
            return View(frm);
        }

        [HttpPost]
        public IActionResult FormSubmitJqueryAjax(FormViewModel model)
        {
            FormViewModel formViewModel = new FormViewModel();

            formViewModel.UserName = model.UserName;
            formViewModel.UserPassword = model.UserPassword;
            formViewModel.UserCountry = model.UserCountries.Where(x => x.Value == model.UserCountry).FirstOrDefault().Text;
            formViewModel.UserGender = model.UserGender;
            formViewModel.AcceptsTerms = model.AcceptsTerms;
            formViewModel.UserSubjects = model.UserSubjects.Where(x => x.IsChecked == true).ToList();

            return PartialView("_FormSubmitJqueryAjaxPartialView", formViewModel); // Return a partial view or HTML content
        }

        public IActionResult FormAddRemoveControls()
        {
            List<ContactVM> contactList = new List<ContactVM>();
            ContactVM contact = new ContactVM();
            var contacts = contact.GetContacts();
            var phones = contact.GetPhones();
            var mapPhonePhoneAttributes = contact.GetMapPhoneAndPhoneAttributes();
            var phoneAttributes = contact.GetPhoneAttributes();

            contactList = (
                         from c in contacts
                         join ph in phones on c.Id equals ph.ContactId
                         join mapPhAttr in mapPhonePhoneAttributes on ph.Id equals mapPhAttr.PhoneId
                         join attr in phoneAttributes on mapPhAttr.PhoneAttributeId equals attr.Id
                         orderby c.Id
                         select new ContactVM() { 
                             ContactId=c.Id,
                             ContactEmail=c.Email,
                             ContactName=c.Name,
                             PhoneId=ph.Id,
                             PhoneNumber=ph.Number,
                             PhoneAttributeId=attr.Id,
                             PhoneAttributeName=attr.Name
                         }
                     ).ToList();

            return View(contactList);
        }

        public IActionResult GetAddEditContact(int contactId)
        {
            AddEditContactVM addEditContact = new AddEditContactVM();
            ContactVM contact = new ContactVM();
            var contacts = contact.GetContacts();
            var phones = contact.GetPhones();
            var mapPhonePhoneAttributes = contact.GetMapPhoneAndPhoneAttributes();
            var phoneAttributes = contact.GetPhoneAttributes();

            var qList = (
                         from c in contacts
                         join ph in phones on c.Id equals ph.ContactId 
                         join mapPhAttr in mapPhonePhoneAttributes on ph.Id equals mapPhAttr.PhoneId
                         join attr in phoneAttributes on mapPhAttr.PhoneAttributeId equals attr.Id
                         orderby c.Id
                         where c.Id == contactId
                         group new { c, ph, mapPhAttr, attr } by new { c.Id } into cgrp
                         select new
                         {
                             con = cgrp.Select(x => x.c).Distinct(),
                             pho = cgrp.Select(x => x.ph).Distinct(),
                             map = cgrp.Select(x=>x.mapPhAttr).Distinct(),
                             att = cgrp.Select(x => x.attr).Distinct()
                         }
                     ).ToList();

            foreach (var data in qList)
            {
                addEditContact.ContactId = data.con.FirstOrDefault().Id;
                addEditContact.ContactName = data.con.FirstOrDefault().Name;
                addEditContact.ContactEmail = data.con.FirstOrDefault().Email;
                var phoneList = data.pho.ToList();
                foreach (var phone in phoneList)
                {
                    var mapList = data.map.ToList();
                    var contactPhoneAttributes = new List<ContactPhoneAttribute>();
                    foreach (var phAttr in phoneAttributes)
                    {
                        var contactPhoneAttribute = new ContactPhoneAttribute()
                        {
                            Id = phAttr.Id,
                            Name = phAttr.Name,
                            IsChecked = mapList.Any(x=>x.PhoneAttributeId == phAttr.Id && x.PhoneId == phone.Id)
                        };
                        contactPhoneAttributes.Add(contactPhoneAttribute);
                    }
                    addEditContact.PhoneWithPhoneAttributes.Add(
                        new PhoneWithPhoneAttributes()
                        {
                            Id = phone.Id,
                            Number = phone.Number,
                            ContactPhoneAttributes = contactPhoneAttributes
                        }
                     );
                }
            }

            if (qList.Count == 0)
            {
                var contactPhoneAttributes = new List<ContactPhoneAttribute>();
                foreach (var phAttr in phoneAttributes)
                {
                    var contactPhoneAttribute = new ContactPhoneAttribute()
                    {
                        Id = phAttr.Id,
                        Name = phAttr.Name,
                        IsChecked = false
                    };
                    contactPhoneAttributes.Add(contactPhoneAttribute);
                }
                addEditContact.PhoneWithPhoneAttributes.Add(
                    new PhoneWithPhoneAttributes()
                    {
                        ContactPhoneAttributes = contactPhoneAttributes
                    }
                 );
            }

            return View(addEditContact);
        }

        public IActionResult AddPhoneWithPhoneAttributes(AddEditContactVM addEditContactVM)
        {
            ModelState.Clear();
            ContactVM contact = new ContactVM();
            var phoneAttributes = contact.GetPhoneAttributes();
            var contactPhoneAttributes = new List<ContactPhoneAttribute>();
            foreach (var phAttr in phoneAttributes)
            {
                var contactPhoneAttribute = new ContactPhoneAttribute()
                {
                    Id = phAttr.Id,
                    Name = phAttr.Name,
                    IsChecked = false
                };
                contactPhoneAttributes.Add(contactPhoneAttribute);
            }
            var newPhoneId = addEditContactVM.PhoneWithPhoneAttributes.Count + 1;
            addEditContactVM.PhoneWithPhoneAttributes.Add(
                new PhoneWithPhoneAttributes()
                {
                    Id = newPhoneId,
                    Number = null,
                    ContactPhoneAttributes = contactPhoneAttributes
                }
            );

            return PartialView("_PhoneWithPhoneAttributesPV", addEditContactVM);
        }

        public IActionResult RemovePhoneWithPhoneAttributes(int phoneId, AddEditContactVM addEditContactVM)
        {
            //you posting back you model, and the values of your model are added to ModelState by the DefaultModeBinder
            //You could solve this by using ModelState.Clear() before returning the view
            ModelState.Clear();
            var phoneWithPhoneAttribute = addEditContactVM.PhoneWithPhoneAttributes.Where(x => x.Id == phoneId).FirstOrDefault();
            if (phoneWithPhoneAttribute != null)
            {
                addEditContactVM.PhoneWithPhoneAttributes.Remove(phoneWithPhoneAttribute);
                var k = ModelState.Keys;
            }
            return PartialView("_PhoneWithPhoneAttributesPV", addEditContactVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetAddEditContact(AddEditContactVM addEditContact)
        {
            if (ModelState.IsValid)
            {
                return Json(new { success = true });
            }
            else
            {
                return View(addEditContact);
            }
        }

        public IActionResult MultiSelect()
        {
            MultiSelect model = new MultiSelect();
            return View(model);
        }

        [HttpPost]
        public IActionResult MultiSelect(MultiSelect model)
        {
            return View(model);
        }

        public IActionResult RemoveServerValidation()
        {
            var model = new RemoveServerValidationModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveServerValidation(RemoveServerValidationModel model)
        {
            if (model.NeedCard == false)
            {
                ModelState.Remove(nameof(model.CardNumber));
                ModelState.Remove(nameof(model.CVV));
            }

            if (ModelState.IsValid)
            {
                ViewBag.FormSucceedMsg = "Form was submitted.";
            }
            
            return View(model);
        }

        public IActionResult ListWithMultiControls()
        {
            List<ListWithMultiControlsModel> model = new List<ListWithMultiControlsModel>
            {
                new ListWithMultiControlsModel
                {
                    UserId = 1,
                    UserName = "Aman",
                    UserCountries = new List<SelectListItem>
                    {
                        new SelectListItem("--Please Select--", ""),
                        new SelectListItem("India", "Ind"),
                        new SelectListItem("Pakistan", "PK"),
                        new SelectListItem("France", "FR")
                    },
                    UserCountryCode = "Ind",
                    UserGenders = new List<SelectListItem>
                    {
                        new SelectListItem("Male", "M"),
                        new SelectListItem("Female", "F"),
                        new SelectListItem("No Disclose", "N")
                    },
                    UserGenderCode = "F",
                    UserHobbies = new List<ListWithMultiControlCB>
                    {
                        new ListWithMultiControlCB
                        {
                            HobbyId = 1,
                            HobbyLabel = "Cricket",
                            IsChecked = true
                        },
                        new ListWithMultiControlCB
                        {
                            HobbyId = 2,
                            HobbyLabel = "Hockey",
                            IsChecked = false
                        },
                        new ListWithMultiControlCB
                        {
                            HobbyId = 3,
                            HobbyLabel = "Football",
                            IsChecked = false
                        }
                    }
                },
                new ListWithMultiControlsModel
                {
                    UserId = 2,
                    UserName = "Bman",
                    UserCountries = new List<SelectListItem>
                    {
                        new SelectListItem("--Please Select--", ""),
                        new SelectListItem("India", "Ind"),
                        new SelectListItem("Pakistan", "PK"),
                        new SelectListItem("France", "FR")
                    },
                    UserCountryCode = "PK",
                    UserGenders = new List<SelectListItem>
                    {
                        new SelectListItem("Male", "M"),
                        new SelectListItem("Female", "F"),
                        new SelectListItem("No Disclose", "N")
                    },
                    UserGenderCode = "M",
                    UserHobbies = new List<ListWithMultiControlCB>
                    {
                        new ListWithMultiControlCB
                        {
                            HobbyId = 1,
                            HobbyLabel = "Cricket",
                            IsChecked = false
                        },
                        new ListWithMultiControlCB
                        {
                            HobbyId = 2,
                            HobbyLabel = "Hockey",
                            IsChecked = true
                        },
                        new ListWithMultiControlCB
                        {
                            HobbyId = 3,
                            HobbyLabel = "Football",
                            IsChecked = false
                        }
                    }
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ListWithMultiControls(ListWithMultiControlsModel model)
        {
            return View(model);
        }

        public IActionResult PartialRefresh()
        {
            PartialRefreshVM model = new PartialRefreshVM();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult RegCustomer(PartialRefreshVM model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CustomerRegister",model);
            }
            return PartialView("_Successs");
        }

        public IActionResult GoToCustRegForm()
        {
            PartialRefreshVM model = new PartialRefreshVM();
            return PartialView("_CustomerRegister", model);
        }
        
        public IActionResult Payment()
        {
            PaymentModel model = new PaymentModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Payment(PaymentModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "ModelState error";
                return View(model);
            }
            return View(model);
        }
    }
}
