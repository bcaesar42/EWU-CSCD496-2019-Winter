using System;
using System.Collections.Generic;

namespace BlogEngine.Api.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsPublished { get; set; }
        public int AuthorId { get; set; }
        public UserViewModel Author { get; set; }
        public string Slug { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<PostTagViewModel> PostTags { get; set; }
    }
}