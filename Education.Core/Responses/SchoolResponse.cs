using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class SchoolResponse : _BaseResponse<SchoolDto>
    {
        public SchoolResponse(SchoolDto Extra) : base(Extra)
        {
        }

        public SchoolResponse(string message) : base(message)
        {
        }
    }
}
