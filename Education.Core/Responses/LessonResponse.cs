using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class LessonResponse : _BaseResponse<LessonDto>
    {
        public LessonResponse(LessonDto Extra) : base(Extra)
        {
        }

        public LessonResponse(string message) : base(message)
        {
        }
    }
}
