using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;
using BlogEngine.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Domain.Services
{
    public class PostService : IPostService
    {
        private ApplicationDbContext DbContext { get; }
        public PostService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<Post> CreatePost(Post post)
        {
            DbContext.Posts.Add(post);
            await DbContext.SaveChangesAsync();

            return post;
        }

        public async Task DeletePost(int id)
        {
            var post = DbContext.Posts.Find(id);
            DbContext.Posts.Remove(post);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await DbContext.Posts.FindAsync(id);
            return post;
        }

        public async Task<ICollection<Post>> GetPosts()
        {
            var posts = await DbContext.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> UpdatePost(Post post)
        {
            DbContext.Posts.Update(post);
            await DbContext.SaveChangesAsync();

            return post;
        }
    }
}