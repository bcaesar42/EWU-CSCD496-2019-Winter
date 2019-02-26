using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;

namespace BlogEngine.Domain.Services.Interfaces
{
    public interface IPostService
    {
        Task<Post> GetPostById(int id);
        Task<ICollection<Post>> GetPosts();
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task DeletePost(int id);
    }
}