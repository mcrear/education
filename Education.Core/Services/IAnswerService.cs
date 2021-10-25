using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IAnswerService : IService<Answer>
    {
        Task<Answer> GetWithQuestionsByIdAsync(Guid Id);
        Task<_BaseResponse<IEnumerable<Answer>>> FindFullAsync(Answer answer);
        new Task<_BaseResponse<IEnumerable<Answer>>> GetAllAsync();
        Task<AnswerListResponse> FindAsync(bool IncludeDeletes);
        Task<AnswerListResponse> FindAsync(AnswerDto answerDto, bool IncludeDeletes);
        Task<AnswerResponse> AddAsync(AnswerDto answerDto, Guid userId);
        AnswerResponse Update(AnswerDto answerDto, Guid userId);
        Task<AnswerResponse> Update(Guid Id, Guid userId);
    }
}
