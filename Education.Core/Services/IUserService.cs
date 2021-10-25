using Education.Core.DTOs;
using Education.Core.Models;
using Education.Core.Responses;
using System;
using System.Threading.Tasks;

namespace Education.Core.Services
{
    public interface IUserService : IService<User>
    {
        Task<UserResponse> GetResponseByIdAsync(Guid Id);
        Task<UserResponse> FindByEmailAndPassword(string email, string password);
        Task SaveRefreshToken(Guid userId, string refreshToken, int AddDay);
        Task<UserResponse> GetUserByRefreshToken(string refreshToken);
        Task RemoveRefreshToken(User user);
        new Task<UserListResponse> GetAllAsync();
        // new Task<UserListResponse> FindAsync(Expression<Func<User, bool>> predicate);
        Task<UserListResponse> FindAsync(bool IncludeDeletes);
        Task<UserListResponse> FindAsync(UserDto userDto, bool IncludeDeletes);
        Task<UserResponse> AddAsync(UserDto userDto, Guid userId);
        UserResponse Update(UserDto userDto, Guid userId);
        Task<UserResponse> Update(Guid Id, Guid userId);
    }
}
