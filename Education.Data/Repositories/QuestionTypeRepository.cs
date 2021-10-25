using Education.Core.Models;
using Education.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Repositories
{
    public class QuestionTypeRepository : Repository<QuestionType>, IQuestionTypeRepository
    {
        public QuestionTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
