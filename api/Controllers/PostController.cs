using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHost;
        public readonly IMapper _mapper;

        public PostController(DataContext context, IWebHostEnvironment webHost, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _webHost = webHost;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuccessDto>> DeletePost(int id){
            
            Post post = await _context.Posts.SingleOrDefaultAsync(p=>p.Id==id);
            
            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();

             SuccessDto response = new SuccessDto{
                Message = "Deleted successfully",
                Data = _mapper.Map<PostDto>(post)
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SuccessDto>> GetPost(int id){
            
            Post post = await _context.Posts.Include(c => c.Comments).Include(l => l.Likes).SingleOrDefaultAsync(p=>p.Id==id);
           
             SuccessDto response = new SuccessDto{
                Message = "Request was successful",
                Data = _mapper.Map<PostDto>(post)
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SuccessDto>> Create([FromForm] CreatePostDto createPostDto){

         string UploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

            if (!Directory.Exists(UploadsFolder))
            {
                Directory.CreateDirectory(UploadsFolder);
            }

            string FileName = Path.GetFileName(createPostDto.File.FileName);
            string FileSavePath = Path.Combine(UploadsFolder, FileName);

            using (FileStream Stream = new FileStream(FileSavePath, FileMode.Create))
            {
              await createPostDto.File.CopyToAsync(Stream);
             
            }

           
            
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

              Post post = new Post{
               UserId = user.Id,
               Author = user.UserName,
               Title = createPostDto.Title,
               Content = createPostDto.Content,
               Category = createPostDto.Category,
               FilePath = $"uploads/{FileName}"
             };

            _context.Posts.Add(post);

            await _context.SaveChangesAsync();
             
            

            SuccessDto response = new SuccessDto{
                Message = "Post created successfully",
                Data = _mapper.Map<PostDto>(post)
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<SuccessDto>> Update([FromForm] UpdatePostDto updatePostDto){
         Post post = await _context.Posts.SingleOrDefaultAsync(p=>p.Id==updatePostDto.Id);

         if (post == null) return BadRequest("Invalid post id");

         if (updatePostDto.File != null)
         {
            string UploadsFolder = Path.Combine(_webHost.WebRootPath, "uploads");

            if (!Directory.Exists(UploadsFolder))
            {
                Directory.CreateDirectory(UploadsFolder);
            }

            string FileName = Path.GetFileName(updatePostDto.File.FileName);
            string FileSavePath = Path.Combine(UploadsFolder, FileName);

            using (FileStream Stream = new FileStream(FileSavePath, FileMode.Create))
            {
              await updatePostDto.File.CopyToAsync(Stream);
             
            }

            post.FilePath = $"uploads/{FileName}";
         }

           post.UpdatedAt = DateTime.Now;

           Post updatedPost = _mapper.Map(updatePostDto,post);
           _context.Posts.Update(updatedPost);

            await _context.SaveChangesAsync();
             
            

            SuccessDto response = new SuccessDto{
                Message = "Post updated successfully",
                Data = _mapper.Map<PostDto>(updatedPost)
            };

            return Ok(response);
        }
    }
}