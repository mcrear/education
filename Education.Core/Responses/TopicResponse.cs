using Education.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Core.Responses
{
    public class TopicResponse : _BaseResponse<TopicDto>
    {
        public TopicResponse(TopicDto Extra) : base(Extra)
        {
        }

        public TopicResponse(string message) : base(message)
        {
        }
    }
}
