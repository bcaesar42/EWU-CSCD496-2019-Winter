using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;
using BlogEngine.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Domain.Services
{
    public class CommentService : ICommentService
    {
        private ApplicationDbContext DbContext { get; }
        public CommentService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<Comment> CreateComment(Comment comment)
        {
            DbContext.Comments.Add(comment);
            await DbContext.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await DbContext.Comments.FindAsync(id);
            return comment;
        }

        public async Task<ICollection<Comment>> GetComments()
        {
            var comments = await DbContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<ICollection<Comment>> GetCommentsForPost(int postId)
        {
            var comments = await DbContext.Comments.Where(c => c.PostId == postId).ToListAsync();
            return comments;
        }
    }
}