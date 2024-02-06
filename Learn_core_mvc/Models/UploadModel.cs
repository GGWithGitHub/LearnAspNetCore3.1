using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class UploadModel
    {
        public int ImageId { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class DataWithFileModel
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
        public int PersonAge { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
