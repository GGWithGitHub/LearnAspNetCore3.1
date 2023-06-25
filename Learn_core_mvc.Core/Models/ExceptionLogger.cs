using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class ExceptionLogger
    {
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime LogTime { get; set; }
    }
}
