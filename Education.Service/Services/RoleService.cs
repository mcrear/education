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
    public class RoleService : Service<Role>, IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<MapRolePermission> _permissionRepository;
        public RoleService(IUnitOfWork unitOfWork, IRepository<Role> repository, IRepository<MapRolePermission> permissionRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            _mapper = mapper;
            _permissionRepository = permissionRepository;
        }
        public async Task<RoleListResponse> GetRolesByUserId(Guid userId)
        {
            try
            {
                List<RoleDto> roleList = new List<RoleDto>();
                var roles = await _unitOfWork.Roles.GetRolesByUserIdAsync(userId);
                foreach (var item in roles)
                {
                    roleList.Add(_mapper.Map<Role, RoleDto>(item));
                }
                return new RoleListResponse(roleList);
            }
            catch (Exception ex)
            {
                return new RoleListResponse($"Roller bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<RoleResponse> AddAsync(RoleDto roleDto, Guid userId)
        {
            try
            {
                List<Guid> PermissionList = new List<Guid>();
                Role role = _mapper.Map<RoleDto, Role>(roleDto);
                role.CreatedBy = userId;
                role = await base.AddAsync(role);
                roleDto = _mapper.Map<Role, RoleDto>(role);
                foreach (var item in roleDto.Permissions)
                {
                    await _permissionRepository.AddAsync(new MapRolePermission
                    {
                        PermissionId = item,
                        RoleId = roleDto.Id
                    });
                    PermissionList.Add(item);
                }
                if (roleDto != null)
                    return new RoleResponse(roleDto);
                else return new RoleResponse("Yeni rol ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Rol eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<RoleListResponse> GetAllAsync()
        {
            try
            {
                List<RoleDto> Roles = new List<RoleDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Roles.Add(_mapper.Map<Role, RoleDto>(item));
                }
                return new RoleListResponse(Roles);
            }
            catch (Exception ex)
            {
                return new RoleListResponse($"Roller listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<RoleResponse> Update(RoleDto roleDto, Guid userId)
        {
            try
            {
                List<Guid> PermissionList = new List<Guid>();
                var role = await GetByIdAsync(roleDto.Id);
                role.UpdatedBy = userId;
                role.UpdateDate = DateTime.Now;
                _unitOfWork.Roles.Update(new Role
                {
                    CreateDate = role.CreateDate,
                    DeleteDate = role.DeleteDate,
                    CreatedBy = role.CreatedBy,
                    DeletedBy = role.DeletedBy,
                    Id = role.Id,
                    IsActive = role.IsActive,
                    IsDeleted = role.IsDeleted,
                    RoleName = roleDto.RoleName,
                    UpdateDate = DateTime.Now,
                    UpdatedBy = userId
                });

                if (role.Permissions != null && role.Permissions.Count > 0)
                {
                    var oldPermissionList = _permissionRepository.FindAsync(x => x.RoleId == roleDto.Id).Result;
                    foreach (var item in oldPermissionList)
                    {
                        _permissionRepository.Remove(item);
                    }

                    foreach (var item in roleDto.Permissions)
                    {
                        await _permissionRepository.AddAsync(new MapRolePermission
                        {
                            PermissionId = item,
                            RoleId = roleDto.Id
                        });
                        PermissionList.Add(item);
                    }
                }

                await _unitOfWork.CommitAsync();

                var responseRole = await GetByIdAsync(roleDto.Id);
                var responseRoleDto = _mapper.Map<Role, RoleDto>(responseRole);
                responseRoleDto.Permissions = PermissionList;


                if (roleDto != null)
                    return new RoleResponse(responseRoleDto);
                else return new RoleResponse("Rol güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Rol güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<RoleListResponse> FindAsync(Expression<Func<Role, bool>> predicate)
        {
            try
            {
                List<RoleDto> Roles = new List<RoleDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Roles.Add(_mapper.Map<Role, RoleDto>(item));
                }
                return new RoleListResponse(Roles);
            }
            catch (Exception ex)
            {
                return new RoleListResponse($"Roller listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }
        public async Task<RoleListResponse> FindAsync(RoleDto roleDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression roleFilter = Expression.Parameter(typeof(Role), "s");
                BinaryExpression exp = null;
                if (roleDto.Id != null && roleDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(roleFilter, "Id");
                    var val1 = Expression.Constant(roleDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }
                if (roleDto.RoleName != null)
                {
                    Expression roleNameProperty = Expression.Property(roleFilter, "RoleName");
                    var val2 = Expression.Constant(roleDto.RoleName);
                    Expression e1 = Expression.Equal(roleNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);

                }
                if (roleDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(roleFilter, "IsActive");
                    var val1 = Expression.Constant(roleDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(roleFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }
                var lambda = Expression.Lambda<Func<Role, bool>>(exp, roleFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new RoleListResponse($"Roller listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<RoleListResponse> FindAsync(bool IncludeDeletes)
        {
            RoleDto sdo = new RoleDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<RoleResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                Role role = await base.GetByIdAsync(Id);
                role.DeletedBy = userId;
                role.DeleteDate = DateTime.Now;
                role.IsDeleted = true;
                role = base.Update(role);
                RoleDto roleDto = _mapper.Map<Role, RoleDto>(role);

                if (roleDto != null)
                    return new RoleResponse(roleDto);
                else return new RoleResponse("Rol silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Rol silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
