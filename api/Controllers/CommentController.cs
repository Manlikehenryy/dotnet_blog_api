using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
         private readonly DataContext _context;
         private readonly IMapper _mapper;
         public CommentController(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SuccessDto>> Create(CreateCommentDto createCommentDto){  
            
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

              Comment comment = new Comment{
               UserId = user.Id,
               PostId = createCommentDto.PostId,
               Author = user.UserName,
               Comment_ = createCommentDto.Comment
             };

            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();
             
            

            SuccessDto response = new SuccessDto{
                Message = "Comment created successfully",
                Data = _mapper.Map<CommentDto>(comment)
            };

            return Ok(response);
        }
    }
}