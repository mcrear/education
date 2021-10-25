using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class QuestionTypeResponse : _BaseResponse<QuestionTypeDto>
    {
        public QuestionTypeResponse(QuestionTypeDto Extra) : base(Extra)
        {
        }

        public QuestionTypeResponse(string message) : base(message)
        {
        }
    }
}
