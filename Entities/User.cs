using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        [Required,StringLength(30)]
        public string Name { get; set; }
        [Required, StringLength(30)]
        public string Surname { get; set; }
        [Required, StringLength(70),EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(4),MaxLength(8)]       
        public string Password { get; set; }
        [Required, MinLength(9)]
        public string PhoneNumber { get; set; }
        [Required, StringLength(300)]
        public string Address { get; set; }
        [Required]
        public string Role { get;set; }             
    }
}
