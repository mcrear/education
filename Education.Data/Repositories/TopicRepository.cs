using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Education.Data.Repositories
{
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        public AppDbContext appDbContext { get => _context as AppDbContext; }
        public TopicRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
