using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Repositories
{
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ExamRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
