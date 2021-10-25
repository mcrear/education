using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class ClassroomListResponse : _BaseResponse<IEnumerable<ClassroomDto>>
    {
        public ClassroomListResponse(IEnumerable<ClassroomDto> Extra) : base(Extra)
        {
        }

        public ClassroomListResponse(string message) : base(message)
        {
        }
    }
}
