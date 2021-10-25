using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class PermissionListResponse : _BaseResponse<IEnumerable<PermissionDto>>
    {
        public PermissionListResponse(IEnumerable<PermissionDto> Extra) : base(Extra)
        {
        }

        public PermissionListResponse(string message) : base(message)
        {
        }
    }
}
