using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public AppDbContext appDbContext { get => _context as AppDbContext; }
        public RoleRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(Guid UserId)
        {
            List<Role> roles = new List<Role>();
            var user = await appDbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == UserId);
            foreach (var item in user.Roles)
            {
                roles.Add(await GetByIdAsync(item.RoleId));
            }
            return roles;
        }
    }
}
