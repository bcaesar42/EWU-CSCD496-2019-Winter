using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;

namespace BlogEngine.Domain.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetCommentById(int id);
        Task<ICollection<Comment>> GetComments();
        Task<ICollection<Comment>> GetCommentsForPost(int postId);
        Task<Comment> CreateComment(Comment comment);
    }
}