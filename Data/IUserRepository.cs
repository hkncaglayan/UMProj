using Data.Domain;
using System;
using System.Collections.Generic;

namespace Data
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User UpdateUser(User user);
        User DeletedUser(int id);
        User GetUserById(int id);
        List<User> GetUsers();
    }
}
