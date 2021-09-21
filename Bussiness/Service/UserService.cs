using Bussiness.DTO;
using Data;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Service
{
    public class UserService : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public UserResponseDTO CreateUser(UserCreateRequestDTO model)
        {
          
                throw new Exception("Kullanıcı Adı Boş Olamaz.");           
        }

        public UserResponseDTO DeleteUser(UserDeleteRequestDTO model)
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO GetUserById(UserGetUserRequestDTO userRequestDto)
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO GetUsers()
        {
            throw new NotImplementedException();
        }

        public UserResponseDTO UpdateUser(UserUpdateRequestDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
