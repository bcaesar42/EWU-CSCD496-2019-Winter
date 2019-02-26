using System.Collections.Generic;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;

namespace BlogEngine.Domain.Services.Interfaces
{
    public interface ITagService
    {
        Task<Tag> GetTagById(int id);
        Task<ICollection<Tag>> GetTags();
        Task<ICollection<Tag>> GetTagsForPost(int postId);
        Task<Tag> CreateTag(Tag tag);

    }
}