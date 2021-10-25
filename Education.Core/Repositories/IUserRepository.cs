using Education.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Education.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindByEmailAndPassword(string email, string password);
        Task SaveRefreshToken(Guid userId, string refreshToken, int AddDay);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task RemoveRefreshToken(User user);
    }
}
