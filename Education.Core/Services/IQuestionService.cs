using Education.Core.Models;
using Education.Core.Request;
using Education.Core.Responses;
using System;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IQuestionService : IService<Question>
    {
        Task<QuestionResponse> GetWithAnswersByIdAsync(Guid Id);
        Task<QuestionResponse> AddWithAnswersAsync(QuestionRequest question, Guid UserId);
    }
}
