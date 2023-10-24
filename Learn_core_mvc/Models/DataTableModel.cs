using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class FillModelFromDt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsQuit { get; set; }
    }
}
