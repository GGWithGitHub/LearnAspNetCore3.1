using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class DatatableResponse
    {
        public int TotalRecords { get; set; }
        public int TotalRecordWithFilter { get; set; }
        public List<DatatableEmp> Data { get; set; }
    }
}
