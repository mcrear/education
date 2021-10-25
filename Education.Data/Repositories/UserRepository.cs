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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public AppDbContext appDbContext { get => _context as AppDbContext; }
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> FindByEmailAndPassword(string email, string password)
        {
            var userList = await FindAsync(x => x.Email == email & x.Password == password);
            var user = userList.FirstOrDefault();
            return user;
        }

        public async Task SaveRefreshToken(Guid userId, string refreshToken, int AddDay)
        {
            var userList = await this.FindAsync(x => x.Id == userId);
            var newUser = userList.FirstOrDefault();

            newUser.RefreshToken = refreshToken;
            newUser.RefreshTokenEndDate = DateTime.Now.AddDays(AddDay);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            var userList = await FindAsync(x => x.RefreshToken == refreshToken);
            var user = userList.FirstOrDefault();
            return user;
        }

        public async Task RemoveRefreshToken(User user)
        {
            var userList = await this.FindAsync(x => x.Id == user.Id);
            var newUser = userList.FirstOrDefault();
            newUser.RefreshToken = null;
            newUser.RefreshTokenEndDate = null;
        }
    }
}
