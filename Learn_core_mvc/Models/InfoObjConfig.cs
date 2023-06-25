using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class InfoObjConfig
    {
        public string Key1 { get; set; }
        public string Key2 { get; set; }
        public InfoObjKey3Config Key3 { get; set; }
    }

    public class InfoObjKey3Config
    {
        public string Key3obj1 { get; set; }
    }
}
