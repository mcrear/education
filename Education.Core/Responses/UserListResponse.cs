using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class UserListResponse : _BaseResponse<IEnumerable<UserDto>>
    {
        public UserListResponse(IEnumerable<UserDto> Extra) : base(Extra)
        {
        }

        public UserListResponse(string message) : base(message)
        {
        }
    }
}
