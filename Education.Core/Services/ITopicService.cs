using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface ITopicService : IService<Topic>
    {
        new Task<TopicListResponse> GetAllAsync();
        // new Task<TopicListResponse> FindAsync(Expression<Func<Topic, bool>> predicate);
        Task<TopicListResponse> FindAsync(bool IncludeDeletes);
        Task<TopicListResponse> FindAsync(TopicDto topicDto, bool IncludeDeletes);
        Task<TopicResponse> AddAsync(TopicDto topicDto, Guid userId);
        TopicResponse Update(TopicDto topicDto, Guid userId);
        Task<TopicResponse> Update(Guid Id, Guid userId);
    }
}
