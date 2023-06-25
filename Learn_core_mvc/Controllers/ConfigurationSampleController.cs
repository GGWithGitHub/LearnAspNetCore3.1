using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class ConfigurationSampleController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly InfoObjConfig _infoObjConfigOptions;

        public ConfigurationSampleController(IConfiguration configuration,IOptions<InfoObjConfig> infoObjConfigOptions)
        {
            _configuration = configuration;
            _infoObjConfigOptions = infoObjConfigOptions.Value;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index2()
        {
            ViewBag.AppName = _configuration["AppName"];
            ViewBag.infoObjKey1 = _configuration["infoObj:key1"];
            ViewBag.infoObjKey2 = _configuration["infoObj:key2"];
            ViewBag.infoObjKey3key3obj1 = _configuration["infoObj:key3:key3obj1"];
            return View("Index2");
        }

        public IActionResult Index3()
        {
            return View();
        }

        public IActionResult Index4()
        {
            ViewBag.AppName = _configuration.GetValue<string>("AppName");
            ViewBag.infoObjKey1 = _configuration.GetValue<string>("infoObj:key1");
            ViewBag.infoObjKey2 = _configuration.GetValue<string>("infoObj:key2");
            ViewBag.infoObjKey3key3obj1 = _configuration.GetValue<string>("infoObj:key3:key3obj1");
            return View("Index4");
        }

        public IActionResult Index5()
        {
            var infoObjConfig = new InfoObjConfig();
            _configuration.Bind("infoObj", infoObjConfig);

            ViewBag.infoObjKey1 = infoObjConfig.Key1;
            ViewBag.infoObjKey2 = infoObjConfig.Key2;
            ViewBag.infoObjKey3key3obj1 = infoObjConfig.Key3.Key3obj1;

            return View("Index5");
        }

        public IActionResult Index6()
        {
            ViewBag.infoObjKey1 = _infoObjConfigOptions.Key1;
            ViewBag.infoObjKey2 = _infoObjConfigOptions.Key2;
            ViewBag.infoObjKey3key3obj1 = _infoObjConfigOptions.Key3.Key3obj1;
            return View("Index6");
        }
    }
}
