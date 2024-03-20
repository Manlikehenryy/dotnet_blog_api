using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}