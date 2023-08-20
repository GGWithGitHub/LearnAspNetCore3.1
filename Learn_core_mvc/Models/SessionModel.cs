using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class SessionLoginModel
    {
        public long UserId { get; set; }

        [Required(ErrorMessage ="Email is required.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string UserPassword { get; set; }
    }
}
