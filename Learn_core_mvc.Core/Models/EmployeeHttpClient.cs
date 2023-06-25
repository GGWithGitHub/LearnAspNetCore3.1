using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class EmployeeHttpClient
    {
        public int id { get; set; }
        public string employee_name { get; set; }
        public int employee_salary { get; set; }
        public int employee_age { get; set; }
        public string profile_image { get; set; }
    }

    public class OutPutAPI<T>
    {
        public string message { get; set; }
        public string status { get; set; }
        public T data { get; set; }
    }
}
