using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class QuestionResponse : _BaseResponse<QuestionDto>
    {
        public QuestionResponse(QuestionDto Extra) : base(Extra)
        {
        }

        public QuestionResponse(string message) : base(message)
        {
        }
    }
}
