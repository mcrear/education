using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Repositories
{
    public class ClassroomRepository : Repository<Classroom>, IClassroomRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ClassroomRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
