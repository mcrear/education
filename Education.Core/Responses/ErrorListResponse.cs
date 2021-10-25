using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class ErrorListResponse : _BaseResponse<IEnumerable<ErrorDto>>
    {
        public ErrorListResponse(IEnumerable<ErrorDto> Extra) : base(Extra)
        {
        }

        public ErrorListResponse(string message) : base(message)
        {
        }
    }
}
