using System;
using System.Collections.Generic;

namespace BlogEngine.Api.ViewModels
{
    public class PostInputViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsPublished { get; set; }
        public int AuthorId { get; set; }
        public string Slug { get; set; }
    }
}