using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;

namespace BlogEngine.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
        Task<ICollection<User>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
    }
}