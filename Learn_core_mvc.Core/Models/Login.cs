using System;
using System.Collections.Generic;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class Login
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserSalt { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserRole { get; set; }
        public int UserRoleId { get; set; }
        public Guid Token { get; set; }
        public bool RememberMe { get; set; }
    }
}
