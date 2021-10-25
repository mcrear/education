using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class SchoolListResponse : _BaseResponse<IEnumerable<SchoolDto>>
    {
        public SchoolListResponse(IEnumerable<SchoolDto> Extra) : base(Extra)
        {
        }

        public SchoolListResponse(string message) : base(message)
        {
        }
    }
}
