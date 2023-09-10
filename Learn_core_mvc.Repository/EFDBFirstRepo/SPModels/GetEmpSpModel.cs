using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.SPModels
{
    public class GetEmpSpModel
    {
        public int emp_id { get; set; }
        public string emp_name { get; set; }
        public string emp_email { get; set; }
        public string emp_phone { get; set; }
        public string emp_address { get; set; }
        public string emp_city { get; set; }
        public string emp_state { get; set; }
        public string emp_country { get; set; }
    }
}
