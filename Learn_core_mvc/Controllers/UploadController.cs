using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public UploadController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(ImageModel imageModel)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            var dir = Path.Combine(wwwRootPath, "UploadedImage");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var filePath = Path.Combine(dir, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageModel.ImageFile.CopyToAsync(fileStream);
            }

            ViewBag.ImagePath = $"/UploadedImage/{fileName}";

            return View("UploadImage");
        }
    }
}
