using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Entities;
using AutoMapper;

namespace api.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            //<from,to>
            CreateMap<Post,PostDto>();
            CreateMap<UpdatePostDto,Post>();
            CreateMap<Comment,CommentDto>();
            CreateMap<Like,LikeDto>();
            CreateMap<AppUser,AppUserDto>();
        }
    }
}