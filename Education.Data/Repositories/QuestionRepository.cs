using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.Repositories
{
    public class QuestionRepository : Repository<Question>, IQuestionRepository
    {
        public AppDbContext appDbContext { get => _context as AppDbContext; }
        public QuestionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Question> GetWithAnswersByIdAsync(Guid Id)
        {
            return await appDbContext.Questions.Include(x => x.Answers).SingleOrDefaultAsync(x => x.Id == Id);
        }
    }
}
