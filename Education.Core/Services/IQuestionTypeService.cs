using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IQuestionTypeService : IService<QuestionType>
    {
        new Task<QuestionTypeListResponse> GetAllAsync();
        // new Task<QuestionTypeListResponse> FindAsync(Expression<Func<QuestionType, bool>> predicate);
        Task<QuestionTypeListResponse> FindAsync(bool IncludeDeletes);
        Task<QuestionTypeListResponse> FindAsync(QuestionTypeDto questionTypeDto, bool IncludeDeletes);
        Task<QuestionTypeResponse> AddAsync(QuestionTypeDto questionTypeDto, Guid userId);
        QuestionTypeResponse Update(QuestionTypeDto questionTypeDto, Guid userId);
        Task<QuestionTypeResponse> Update(Guid Id, Guid userId);
    }
}
