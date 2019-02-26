using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;
using BlogEngine.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Domain.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext DbContext { get; }
        public UserService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            var users = await DbContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await DbContext.Users.FindAsync(id);
            return user;
        }

        public async Task<User> CreateUser(User user)
        {
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            DbContext.Users.Update(user);
            await DbContext.SaveChangesAsync();

            return user;
        }
    }
}