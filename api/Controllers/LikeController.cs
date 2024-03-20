using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Entities;
using api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public LikeController(DataContext context, IMapper mapper, IUserRepository userRepository,IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

       [HttpGet]
        public Task<string> Get(){
        return _emailService.SendEmailAsync("mbamaluhenry8@gmail.com","First C# Mail","Urgent matter");
        // return _userRepository.GetAuthenticatedUserName();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SuccessDto>> Create(CreateLikeDto createLikeDto){  
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

              Like like = new Like{
               UserId = user.Id,
               PostId = createLikeDto.PostId,           
             };

            _context.Likes.Add(like);

            await _context.SaveChangesAsync();
             
            

            SuccessDto response = new SuccessDto{
                Message = "Like added successfully",
                Data = _mapper.Map<LikeDto>(like)
            };

            return Ok(response);
        }
    }
}