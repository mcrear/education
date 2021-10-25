using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class ClassroomResponse : _BaseResponse<ClassroomDto>
    {
        public ClassroomResponse(ClassroomDto Extra) : base(Extra)
        {
        }

        public ClassroomResponse(string message) : base(message)
        {
        }
    }
}
