using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Entities.ResponseObject
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Adı"), Required, StringLength(30)]
        public string Name { get; set; }
        [DisplayName("Soyadı"), Required, StringLength(30)]
        public string Surname { get; set; }
        [DisplayName("Email Adresi"),Required, StringLength(70), EmailAddress]
        public string Email { get; set; }
        [DisplayName("Parola"), Required, MinLength(4), MaxLength(8)]
        public string Password { get; set; }
        [DisplayName("Telefon Numarası"), Required, MinLength(9)]
        public string PhoneNumber { get; set; }
        [DisplayName("Adres"), Required, StringLength(300)]
        public string Address { get; set; }
        [Required]
        public string Role { get; set; }        
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int LastUpdatedBy { get; set; }
        public string LastUpdatedByName { get; set; }
        
        public HashSet<string> UserRoles { get; set; }
    }
}