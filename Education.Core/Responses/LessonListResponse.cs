using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class LessonListResponse : _BaseResponse<IEnumerable<LessonDto>>
    {
        public LessonListResponse(IEnumerable<LessonDto> Extra) : base(Extra)
        {
        }

        public LessonListResponse(string message) : base(message)
        {
        }
    }
}
