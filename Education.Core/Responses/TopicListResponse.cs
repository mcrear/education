using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class TopicListResponse : _BaseResponse<IEnumerable<TopicDto>>
    {
        public TopicListResponse(IEnumerable<TopicDto> Extra) : base(Extra)
        {
        }

        public TopicListResponse(string message) : base(message)
        {
        }
    }
}
