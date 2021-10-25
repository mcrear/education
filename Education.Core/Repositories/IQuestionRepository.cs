using Education.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Repositories
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<Question> GetWithAnswersByIdAsync(Guid Id);
    }
}
