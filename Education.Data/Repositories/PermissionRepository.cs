using Education.Core.Models;
using Education.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Data.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        public AppDbContext appDbContext { get => _context as AppDbContext; }
        public PermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
        {
            List<Permission> permissions = new List<Permission>();
            var role = await appDbContext.Roles.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == roleId);
            foreach (var item in role.Permissions)
            {
                if (permissions.Where(x => x.Id == item.PermissionId).Count() == 0)
                    permissions.Add(await GetByIdAsync(item.PermissionId));
            }
            return permissions.OrderBy(x => x.CreateDate).ThenBy(x => x.PermissionName);
        }
    }
}
