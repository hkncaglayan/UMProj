using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entities.ResponseObject
{
    public class LoginModel
    {
        [Required]
        [EmailAddress] 
        [MaxLength(50)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(8),MinLength(4)]
        [DataType(DataType.Password)]
        [DisplayName("Parola")]
        public string Password { get; set; }
   
    }
}