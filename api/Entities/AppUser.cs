using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Entities
{
    public class AppUser : IdentityUser<int>
    {
       
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;}
        public ICollection<AppUserRole> UserRoles {get; set;}
        public ICollection<Post> Posts {get; set;}
        // public ICollection<Comment> Comments {get; set;}
        // public ICollection<Like> Likes {get; set;}
    }
}