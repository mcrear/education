using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class RoleResponse : _BaseResponse<RoleDto>
    {
        public RoleResponse(RoleDto Extra) : base(Extra)
        {
        }

        public RoleResponse(string message) : base(message)
        {
        }
    }
}
