using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.Repositories
{
    public class AnswerRepository : Repository<Answer>, IAnswerRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public AnswerRepository(AppDbContext context) : base(context)
        {

        }
        public async Task<Answer> GetWithQuestionsByIdAsync(Guid Id)
        {
            return await appDbContext.Answers.Include(x => x.Question).FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
