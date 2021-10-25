using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IRoleService : IService<Role>
    {
        Task<RoleListResponse> GetRolesByUserId(Guid userId);
        new Task<RoleListResponse> GetAllAsync();
        // new Task<RoleListResponse> FindAsync(Expression<Func<Role, bool>> predicate);
        Task<RoleListResponse> FindAsync(bool IncludeDeletes);
        Task<RoleListResponse> FindAsync(RoleDto roleDto, bool IncludeDeletes);
        Task<RoleResponse> AddAsync(RoleDto roleDto, Guid userId);
        Task<RoleResponse> Update(RoleDto roleDto, Guid userId);
        Task<RoleResponse> Update(Guid Id, Guid userId);
    }
}
