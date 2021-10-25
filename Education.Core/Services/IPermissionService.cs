using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IPermissionService : IService<Permission>
    {
        Task<PermissionListResponse> GetPermissionsByUserId(Guid userId);

        new Task<PermissionListResponse> GetAllAsync();
        // new Task<PermissionListResponse> FindAsync(Expression<Func<Permission, bool>> predicate);
        Task<PermissionListResponse> FindAsync(bool IncludeDeletes);
        Task<PermissionListResponse> FindAsync(PermissionDto permissionDto, bool IncludeDeletes);
        Task<PermissionResponse> AddAsync(PermissionDto permissionDto, Guid userId);
        PermissionResponse Update(PermissionDto permissionDto, Guid userId);
        Task<PermissionResponse> Update(Guid Id, Guid userId);
    }
}
