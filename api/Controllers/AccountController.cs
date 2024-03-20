using System;

using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Entities;
using api.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public readonly SignInManager<AppUser> _signInManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<SuccessDto>> Register(RegisterDto registerDto){
          if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

          var user = new AppUser
          {
            UserName = registerDto.Username.ToLower(),
            Email = registerDto.Email.ToLower(),
            PhoneNumber = registerDto.PhoneNumber
          };

          var result = await _userManager.CreateAsync(user, registerDto.Password);

          if(!result.Succeeded) return BadRequest(result.Errors);

          var response = new SuccessDto
          {
            Message = "User created successfully",
            Data = user
          };

         return Ok(response);

        }

        [HttpPost("login")]
        public async Task<ActionResult<SuccessDto>> Login(LoginDto loginDto){
         var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Invalid username");

         var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if(!result.Succeeded) return Unauthorized();

            UserDto authorizedUser = new UserDto{
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
          };
        var response = new SuccessDto
          {
            Message = "Login was successful",
            Data = authorizedUser
          };

         return Ok(response);
         
        }

        private async Task<bool> UserExists(string username){
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}