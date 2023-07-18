using Learn_core_mvc.Core.Models;
using Learn_core_mvc.Models;
using Learn_core_mvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
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
            }
            return PartialView("_PhoneWithPhoneAttributesPV", addEditContactVM);
        }

        public IActionResult SaveAddEditContact(AddEditContactVM addEditContact)
        {
            return Json(new { success=true });
        }
    }
}
