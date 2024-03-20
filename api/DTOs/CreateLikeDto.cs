using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class CreateLikeDto
    {
        [Required]
        public int PostId { get; set; }
    }
}