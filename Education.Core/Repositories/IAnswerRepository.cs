using Education.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Repositories
{
    public interface IAnswerRepository : IRepository<Answer>
    {
        Task<Answer> GetWithQuestionsByIdAsync(Guid Id);
    }
}
