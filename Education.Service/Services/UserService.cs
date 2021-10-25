
using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Repositories;
using Education.Core.Responses;
using Education.Core.Services;
using Education.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq.Expressions;

namespace Education.Service.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IMapper mapper;
        public UserService(IUnitOfWork unitOfWork, IRepository<User> repository, IMapper mapper) : base(unitOfWork, repository)
        {
            this.mapper = mapper;
        }

        public async Task<UserResponse> GetResponseByIdAsync(Guid Id)
        {
            try
            {
                var user = await base.GetByIdAsync(Id);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı.");
                }
                else
                {
                    var userDto = mapper.Map<User, UserDto>(user);
                    return new UserResponse(userDto);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }

        }

        public async Task<UserResponse> FindByEmailAndPassword(string email, string password)
        {
            try
            {
                User user = await _unitOfWork.Users.FindByEmailAndPassword(email, password);
                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı.");
                }
                else
                {
                    var userDto = mapper.Map<User, UserDto>(user);
                    return new UserResponse(userDto);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<UserResponse> GetUserByRefreshToken(string refreshToken)
        {
            try
            {
                User user = await _unitOfWork.Users.GetUserByRefreshToken(refreshToken);

                if (user == null)
                {
                    return new UserResponse("Kullanıcı bulunamadı.");
                }
                else
                {
                    var userDto = mapper.Map<User, UserDto>(user);
                    return new UserResponse(userDto);
                }
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı bulunurken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task RemoveRefreshToken(User user)
        {
            try
            {
                await _unitOfWork.Users.RemoveRefreshToken(user);
                await _unitOfWork.CommitAsync(); ;
            }
            catch (Exception)
            {
                //loglama yapılacaktır.
            }
        }

        public async Task SaveRefreshToken(Guid userId, string refreshToken, int AddDay)
        {
            try
            {
                await _unitOfWork.Users.SaveRefreshToken(userId, refreshToken, AddDay);

                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                //loglama yapılacaktır..
            }
        }

        public async Task<UserResponse> AddAsync(UserDto userDto, Guid userId)
        {
            try
            {
                User user = mapper.Map<UserDto, User>(userDto);
                user.CreatedBy = userId;
                user = await base.AddAsync(user);
                userDto = mapper.Map<User, UserDto>(user);

                if (userDto != null)
                    return new UserResponse(userDto);
                else return new UserResponse("Yeni kullanıcı ekleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı eklenirken bir hata meydana geldi:{ex.Message}");
            }

        }

        public new async Task<UserListResponse> GetAllAsync()
        {
            try
            {
                List<UserDto> Users = new List<UserDto>();
                var list = await base.GetAllAsync();

                foreach (var item in list)
                {
                    Users.Add(mapper.Map<User, UserDto>(item));
                }
                return new UserListResponse(Users);
            }
            catch (Exception ex)
            {
                return new UserListResponse($"Kullanıcılar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public UserResponse Update(UserDto userDto, Guid userId)
        {
            try
            {
                User user = mapper.Map<UserDto, User>(userDto);
                user.UpdatedBy = userId;
                user.UpdateDate = DateTime.Now;
                user = base.Update(user);
                userDto = mapper.Map<User, UserDto>(user);

                if (userDto != null)
                    return new UserResponse(userDto);
                else return new UserResponse("Kullanıcı güncelleme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı güncellenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        private new async Task<UserListResponse> FindAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                List<UserDto> Users = new List<UserDto>();
                var list = await base.FindAsync(predicate);

                foreach (var item in list)
                {
                    Users.Add(mapper.Map<User, UserDto>(item));
                }
                return new UserListResponse(Users);
            }
            catch (Exception ex)
            {
                return new UserListResponse($"Kullanıcılar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public async Task<UserListResponse> FindAsync(UserDto userDto, bool IncludeDeletes)
        {
            try
            {
                ParameterExpression userFilter = Expression.Parameter(typeof(User), "s");
                BinaryExpression exp = null;
                if (userDto.Id != null && userDto.Id != Guid.Empty)
                {
                    Expression idProperty = Expression.Property(userFilter, "Id");
                    var val1 = Expression.Constant(userDto.Id);
                    Expression e1 = Expression.Equal(idProperty, val1);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }

                if (userDto.FirstName != null)
                {
                    Expression userNameProperty = Expression.Property(userFilter, "FirstName");
                    var val2 = Expression.Constant(userDto.FirstName);
                    Expression e1 = Expression.Equal(userNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }

                if (userDto.LastName != null)
                {
                    Expression userNameProperty = Expression.Property(userFilter, "LastName");
                    var val2 = Expression.Constant(userDto.LastName);
                    Expression e1 = Expression.Equal(userNameProperty, val2);
                    exp = exp == null ? exp = Expression.Or(e1, e1) : exp = Expression.Or(exp, e1);
                }

                if (userDto.IsActive.HasValue)
                {
                    Expression isActiveProperty = Expression.Property(userFilter, "IsActive");
                    var val1 = Expression.Constant(userDto.IsActive.Value);
                    Expression e1 = Expression.Equal(isActiveProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }

                if (!IncludeDeletes)
                {
                    Expression isDeletedProperty = Expression.Property(userFilter, "IsDeleted");
                    var val1 = Expression.Constant(false);
                    Expression e1 = Expression.Equal(isDeletedProperty, val1);
                    exp = exp == null ? exp = Expression.And(e1, e1) : exp = Expression.AndAlso(exp, e1);
                }

                var lambda = Expression.Lambda<Func<User, bool>>(exp, userFilter);
                return await FindAsync(lambda);
            }
            catch (Exception ex)
            {
                return new UserListResponse($"Kullanıcılar listelenirken bir hata meydana geldi:{ex.Message}");
            }
        }

        public Task<UserListResponse> FindAsync(bool IncludeDeletes)
        {
            UserDto sdo = new UserDto();
            return FindAsync(sdo, IncludeDeletes);
        }

        public async Task<UserResponse> Update(Guid Id, Guid userId)
        {
            try
            {
                User user = await base.GetByIdAsync(Id);
                user.DeletedBy = userId;
                user.DeleteDate = DateTime.Now;
                user.IsDeleted = true;
                user = base.Update(user);
                UserDto userDto = mapper.Map<User, UserDto>(user);

                if (userDto != null)
                    return new UserResponse(userDto);
                else return new UserResponse("Kullanıcı silme işlemi sırasında bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return new UserResponse($"Kullanıcı silinirken bir hata meydana geldi:{ex.Message}");
            }
        }
    }
}
