using AutoMapper;
using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Repositories;
using Education.Core.Responses;
using Education.Core.Services;
using Education.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.Services
{
    public class PermissionService : Service<Permission>, IPermissionService
    {
        private readonly IRoleRepository roleRepository;
        public readonly IMapper mapper;
        public PermissionService(IUnitOfWork unitOfWork, IRepository<Permission> repository, IRoleRepository roleRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
            this.roleRepository = roleRepository;
        }

        public async Task<PermissionListResponse> GetPermissionsByUserId(Guid userId)
        {
            try
            {
                List<PermissionDto> permissionList = new List<PermissionDto>();
                var roles = await roleRepository.GetRolesByUserIdAsync(userId);
                foreach (var item in roles)
                {
                    var permissions = await _unitOfWork.Permissions.GetPermissionsByRoleIdAsync(item.Id);
                    foreach (var permission in permissions)
                    {
                        permissionList.Add(mapper.Map<Permission, PermissionDto>(permission));
                    }
                }
                return new PermissionListResponse(permissionList);
            }
            catch (Exception ex)
            {

                return new PermissionListResponse($"Yetkiler bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<PermissionResponse> AddAsync(PermissionDto permissionDto, Guid userId)
        {
            try
            {
                Permission permission = mapper.Map<PermissionDto, Permission>(permissionDto);
                permission.CreatedBy = userId;
                permission = await base.AddAsync(permission);
                permissionDto = mapper.Map<Permission, PermissionDto>(permission);

                if (permissionDto != null)
                    return new PermissionResponse(permissionDto);
                else return new PermissionResponse("Yeni izin ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new PermissionResponse($"İzin eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<PermissionListResponse> GetAllAsync()
        {
            try
            {
                List<PermissionDto> Permissions = new List<PermissionDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Permissions.Add(mapper.Map<Permission, PermissionDto>(item));
                }
               return new PermissionListResponse(Permissions);
            }
            catch (Exception ex)
            {
                return new PermissionListResponse($"İzinler listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public PermissionResponse Update(PermissionDto permissionDto, Guid userId)
        {
            try
            {
                Permission permission = mapper.Map<PermissionDto, Permission>(permissionDto);
                permission.UpdatedBy = userId;
                permission.UpdateDate = DateTime.Now;
                permission = base.Update(permission);
                permissionDto = mapper.Map<Permission, PermissionDto>(permission);

                if (permissionDto != null)
                    return new PermissionResponse(permissionDto);
                else return new PermissionResponse("İzin güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new PermissionResponse($"İzin güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<PermissionListResponse> FindAsync(Expression<Func<Permission, bool>> predicate)
        {
            try
            {
                List<PermissionDto> Permissions = new List<PermissionDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Permissions.Add(mapper.Map<Permission, PermissionDto>(item));
                }
                return new PermissionListResponse(Permissions);
            }
            catch (Exception ex)
            {
                return new PermissionListResponse($"İzinler listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<PermissionListResponse> FindAsync(PermissionDto permissionDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression permissionFilter = Expression.Parameter(typeof(Permission), "s");
                BinaryExpression exp = null;
                if (permissionDto.Id != null && permissionDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(permissionFilter, "Id");
                    var val1 = Expression.Constant(permissionDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (permissionDto.PermissionName != null)
                {
                    Expression permissionNameProperty = Expression.Property(permissionFilter, "PermissionName");
                    var val2 = Expression.Constant(permissionDto.PermissionName);
                    Expression e1 = Expression.Equal(permissionNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (permissionDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(permissionFilter, "IsActive");
                    var val1 = Expression.Constant(permissionDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(permissionFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Permission, bool>>(exp, permissionFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new PermissionListResponse($"İzinler listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<PermissionListResponse> FindAsync(bool IncludeDeletes)
        {
            PermissionDto sdo = new PermissionDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<PermissionResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Permission permission = await base.GetByIdAsync(Id);
                permission.DeletedBy = userId;
                permission.DeleteDate = DateTime.Now;
                permission.IsDeleted = true;
                permission = base.Update(permission);
                PermissionDto permissionDto = mapper.Map<Permission, PermissionDto>(permission);

                if (permissionDto != null)
                    return new PermissionResponse(permissionDto);
                else return new PermissionResponse("İzin silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new PermissionResponse($"İzin silinirken bir hata meydana geldi:{ex.Message}");
            }
        }

    }
}
