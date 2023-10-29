using Learn_core_mvc.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
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

        public IActionResult CompressImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompressImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                using (var image = await Image.LoadAsync(stream))
                {
                    var quality = 50;

                    // Save the compressed image
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine("wwwroot/images", uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.SaveAsync(fileStream, new JpegEncoder { Quality = quality });
                    }

                    byte[] imageBytes = ConvertImageToByteArray(filePath);

                    // Set the file content type
                    var contentType = "image/jpeg"; // Change this according to your image type

                    // Set the file name (you can customize this)
                    var fileName = "compressed_image.jpg";

                    // Send the file to the browser
                    return File(imageBytes, contentType, fileName);
                }
            }
            return View("CompressImage");
        }

        public byte[] ConvertImageToByteArray(string filePath)
        {
            // Check if the file exists
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found.", filePath);
            }

            // Read the file as bytes
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return fileBytes;
        }
    }
}
