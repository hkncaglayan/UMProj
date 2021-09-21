using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            User baseUser = new User()
            {
                Address = "Maltepe/İST",
                Created = DateTime.Now,
                CreatedBy = -1,
                CreatedByName = "SYSTEM",                
                Email = "test@admin.com",
                LastUpdated = DateTime.Now,
                LastUpdatedBy = -1,
                LastUpdatedByName = "SYSTEM",
                PhoneNumber = "05555593974",
                Name = "Admin",
                Surname = "User",
                Password = "admin",
                Role = UserRoles.Admin.ToString()
            };
            context.User.Add(baseUser);
            context.SaveChanges();
            for (int i = 0; i < 5; i++)
            {
                var name = FakeData.NameData.GetFirstName();
                var surname = FakeData.NameData.GetSurname();
                User user = new User()                
                {
                    Address = FakeData.PlaceData.GetAddress(),
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = baseUser.Id,
                    LastUpdatedByName = baseUser.Name,
                    Created = DateTime.Now,
                    CreatedBy = baseUser.Id,
                    CreatedByName = baseUser.Name,
                    Role = UserRoles.User.ToString(),
                    Name = name,
                    Surname = surname,
                    Password = FakeData.TextData.GetAlphabetical(5),                    
                    Email = FakeData.NetworkData.GetEmail(name, surname),
                    PhoneNumber = FakeData.PhoneNumberData.GetPhoneNumber().ToString()
                };
                context.User.Add(user);
                context.SaveChanges();
            }
            
        }
    }
}
