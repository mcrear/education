using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Repositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        public AppDbContext appDbContext { get => _context as AppDbContext; }
        public SchoolRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
