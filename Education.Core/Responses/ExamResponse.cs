using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class ExamResponse : _BaseResponse<ExamDto>
    {
        public ExamResponse(ExamDto Extra) : base(Extra)
        {
        }

        public ExamResponse(string message) : base(message)
        {
        }
    }
}
