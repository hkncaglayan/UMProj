using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.DTO
{
    public abstract class UserBaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
