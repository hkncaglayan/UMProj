using Bussiness.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Service
{
    public interface IUserManager
    {
        UserResponseDTO CreateUser(UserCreateRequestDTO insertModel);
        UserResponseDTO UpdateUser(UserUpdateRequestDTO insertModel);
        UserResponseDTO DeleteUser(UserDeleteRequestDTO insertModel);
        UserResponseDTO GetUserById(UserGetUserRequestDTO userRequestDto);
        UserResponseDTO GetUsers();
    }
}
