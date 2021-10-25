using Education.Core.DTOs;
using Education.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class QuestionTypeListResponse : _BaseResponse<IEnumerable<QuestionTypeDto>>
    {
        public QuestionTypeListResponse(IEnumerable<QuestionTypeDto> Extra) : base(Extra)
        {
        }

        public QuestionTypeListResponse(string message) : base(message)
        {
        }
    }
}
