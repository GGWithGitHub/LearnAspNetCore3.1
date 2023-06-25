using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Learn_core_mvc.Models
{
    public class SignupModelIdentity
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }

        [Compare("UserPassword",ErrorMessage ="Password and confirm password must be same")]
        public string UserConfirmPassword { get; set; }
    }
}
