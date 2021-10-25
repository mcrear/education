using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class AnswerListResponse : _BaseResponse<IEnumerable<AnswerDto>>
    {
        public AnswerListResponse(IEnumerable<AnswerDto> Extra) : base(Extra)
        {
        }

        public AnswerListResponse(string message) : base(message)
        {
        }
    }
}
