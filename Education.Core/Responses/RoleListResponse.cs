using Education.Core.DTOs;
using System.Collections.Generic;

namespace Education.Core.Responses
{
    public class RoleListResponse : _BaseResponse<IEnumerable<RoleDto>>
    {
        public RoleListResponse(IEnumerable<RoleDto> Extra) : base(Extra)
        {
        }

        public RoleListResponse(string message) : base(message)
        {
        }
    }
}
