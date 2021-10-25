using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class UserResponse : _BaseResponse<UserDto>
    {
        public UserResponse(UserDto Extra) : base(Extra)
        {
        }

        public UserResponse(string message) : base(message)
        {
        }
    }
}
