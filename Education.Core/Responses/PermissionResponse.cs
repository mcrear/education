using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class PermissionResponse : _BaseResponse<PermissionDto>
    {
        public PermissionResponse(PermissionDto Extra) : base(Extra)
        {
        }

        public PermissionResponse(string message) : base(message)
        {
        }
    }
}
