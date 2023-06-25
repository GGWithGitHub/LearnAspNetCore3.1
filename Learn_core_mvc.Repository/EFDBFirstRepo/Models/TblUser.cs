using System;
using System.Collections.Generic;

namespace Learn_core_mvc.Repository.EFDBFirstRepo.Models
{
    public partial class TblUser
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserSalt { get; set; }
        public int? UserRoleId { get; set; }
        public Guid? UserToken { get; set; }
    }
}
