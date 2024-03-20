using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.DTOs
{
    public class PostSuccessDto
    {
        public string Status = "Success";
        public string Message { get; set; }
        public Post Data { get; set; }
    }
}