using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Domain.Models;
using BlogEngine.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogEngine.Domain.Services
{
    public class TagService : ITagService
    {
        private ApplicationDbContext DbContext { get; }
        public TagService(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<Tag> CreateTag(Tag tag)
        {
            DbContext.Tags.Add(tag);
            await DbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag> GetTagById(int id)
        {
            var tag = await DbContext.Tags.FindAsync(id);
            return tag;
        }

        public async Task<ICollection<Tag>> GetTags()
        {
            var tags = await DbContext.Tags.ToListAsync();
            return tags;
        }

        public async Task<ICollection<Tag>> GetTagsForPost(int postId)
        {
            var post = await DbContext.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .SingleOrDefaultAsync();

            return post.PostTags.Select(pt => pt.Tag).ToList();
        }
    }
}