using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class DatatableProperties
    {
        public int Draw { get; set; }
        public int Row { get; set; }
        public int RowPerPage { get; set; }
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string ColumnSortOrder { get; set; }
        public string SearchValue { get; set; }

        public string ColName 
        {
            get
            {
                return ColumnName;
            }
            set
            {
                if (value.ToLower() == "empname")
                    ColumnName = "emp_name";
                if (value.ToLower() == "empemail")
                    ColumnName = "emp_email";
                if (value.ToLower() == "empgender")
                    ColumnName = "emp_gender";
                if (value.ToLower() == "empsalary")
                    ColumnName = "emp_salary";
                if (value.ToLower() == "empcity")
                    ColumnName = "emp_city";
            }
        }
    }
}
