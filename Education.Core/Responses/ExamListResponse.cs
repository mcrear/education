using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class ExamListResponse : _BaseResponse<IEnumerable<ExamDto>>
    {
        public ExamListResponse(IEnumerable<ExamDto> Extra) : base(Extra)
        {
        }

        public ExamListResponse(string message) : base(message)
        {
        }
    }
}
