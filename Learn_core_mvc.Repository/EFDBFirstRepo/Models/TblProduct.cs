using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductDesc { get; set; }
        public int? ProductRating { get; set; }
    }
}
