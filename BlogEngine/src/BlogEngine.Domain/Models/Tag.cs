using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogEngine.Domain.Models
{
    public class Tag : Entity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
