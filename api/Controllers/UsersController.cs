using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailService;
        public UsersController(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<AppUser>> GetUsers(){
            return _context.Users.ToList();

        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id){
           var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            var user = _context.Users.Find(id);

            if (user.UserName == username)
            {
                return user;
            }
            else{
                return Unauthorized("You dont have permission");
            }

        }

        [HttpGet]
        public Task<string> Get(){
        return _emailService.SendEmailAsync("email@gmail.com","C# Mail","Urgent matter");
        // return _userRepository.GetAuthenticatedUserName();
        }
    }
}