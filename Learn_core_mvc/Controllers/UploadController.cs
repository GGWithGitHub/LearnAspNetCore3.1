using Learn_core_mvc.Models;
using Learn_core_mvc.Repository;
using Learn_core_mvc.Repository.EFCodeFirst.Models;
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
        private readonly IFileRepository _fileRepo;

        public UploadController(IWebHostEnvironment hostEnvironment, IFileRepository fileRepo)
        {
            _hostEnvironment = hostEnvironment;
            _fileRepo = fileRepo;
        }
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(UploadModel imageModel)
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

        public IActionResult ModelWithFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostModelWithFile(DataWithFileModel model)
        {
            return View("ModelWithFile",model);
        }

        //public async Task<IActionResult> FillModelAndFile()
        //{
        //    DataWithFileModel model = new DataWithFileModel();
        //    model.PersonId = 1;
        //    model.PersonName = "Gaurav";
        //    model.PersonEmail = "gaurav@testing.com";
        //    model.PersonAge = 25;

        //    string wwwRootPath = _hostEnvironment.WebRootPath;
        //    string fileName = Path.GetFileName("Panda.jpg");
        //    var dir = Path.Combine(wwwRootPath, "images");
        //    if (Directory.Exists(dir))
        //    {
        //        var filePath = Path.Combine(dir, fileName);
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            using (var fileStream = new FileStream(filePath, FileMode.Open))
        //            {
        //                await fileStream.CopyToAsync(memoryStream);
        //            }

        //            string contentType = "application/octet-stream"; // Default to binary data
        //            string extension = Path.GetExtension(filePath);
        //            if (!string.IsNullOrEmpty(extension))
        //            {
        //                contentType = GetContentType(extension);
        //            }

        //            model.ImageFile = new FormFile(memoryStream, 0, memoryStream.Length, null, Path.GetFileName(filePath)) {
        //                Headers = new HeaderDictionary(),
        //                ContentType = contentType
        //            };
        //            // Example: Reset the position to the beginning of the stream
        //            memoryStream.Position = 0;
        //        }
        //    }

        //    return View("ModelWithFile",model);
        //}

        private string GetContentType(string fileExtension)
        {
            // Add additional mappings as needed
            switch (fileExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                // Add more cases for other file types...
                default:
                    return "application/octet-stream";
            }
        }

        public async Task<IActionResult> UploadDownloadFiles()
        {
            var files = await _fileRepo.GetAll();
            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    
                    var newFile = new TblFile();
                    newFile.MimeType = file.ContentType;
                    newFile.FileName = Path.GetFileName(file.FileName);
                    newFile.Content = ms.ToArray();
                    
                    await _fileRepo.AddFile(newFile);
                }
            }
            
            return RedirectToAction("UploadDownloadFiles");
        }

        public async Task<FileResult> DownloadFile(string fileId)
        {
            var file = await _fileRepo.GetFile(fileId);
            if (file == null || file.Content == null)
            {
                return null; // Or handle the error appropriately
            }

            Response.Headers.Add("Content-Disposition", $"attachment; filename={file.FileName}");
            return File(file.Content, file.MimeType);
        }
    }
}
