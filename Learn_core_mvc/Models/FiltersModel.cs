using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class FiltersBookModel
    {
        [Required]
        public string BookName { get; set; }

        [Required]
        public string BookPrice { get; set; }
    }
}
