using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class AnswerResponse : _BaseResponse<AnswerDto>
    {
        public AnswerResponse(AnswerDto Extra) : base(Extra)
        {
        }

        public AnswerResponse(string message) : base(message)
        {
        }
    }
}
