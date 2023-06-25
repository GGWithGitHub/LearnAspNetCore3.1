using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Learn_core_mvc.Core.Models
{
    public class RequestPasswordResetModel
    {
        private string email;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [MaxLength(256, ErrorMessage = "Email must be less than 100 characters long.")]
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    this.email = value.ToLower().Trim();
                }
                else
                {
                    this.email = null;
                }
            }
        }
    }
}
