using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class PostDto
    {
    
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Author {get; set;}
        public string Title { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }
        public string Category {get; set;}
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;}
        public ICollection<CommentDto> Comments { get; set; }
        public ICollection<LikeDto> Likes { get; set; }
    }
}