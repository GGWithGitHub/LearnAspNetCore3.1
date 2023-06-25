using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class Register
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public int UserRoleId { get; set; }
    }
}
