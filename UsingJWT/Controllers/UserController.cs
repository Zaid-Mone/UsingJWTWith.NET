using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsingJWT.Data;
using UsingJWT.DTOs;
using UsingJWT.Models;
using UsingJWT.Services;

namespace UsingJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        public UserController(DataDbContext context, UserManager<User> userManager, IConfiguration config, ITokenGenerator tokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("test")]
        public async Task<ActionResult> CreateUser()
        {
            var result = await _userManager.CreateAsync(new Models.User
            {
                 Email="Admin@email.com",
                 FirstName="Admin",
                 UserName="Admin",
                 SecurityStamp=Guid.NewGuid().ToString()
            },"Admin1234*");
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginDTOResponse>> Post(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user == null)
            {
                return BadRequest(new { message="User is not exist please register"});
            }
            var result = await _userManager.CheckPasswordAsync(user,loginDTO.Password);

            if (result)
            {
                return Ok(new LoginDTOResponse
                {
                    Token = _tokenGenerator.CreateToken(user)
                }) ;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisterDTO>> Post(RegisterDTO registerDTO)
        {
            var user = new User
            {
                UserName =registerDTO.Email,
                FirstName=registerDTO.FirstName,
                Email=registerDTO.Email,
            };
            var check = await _userManager.FindByNameAsync(registerDTO.Email);
            if (check!=null)
            {
                return BadRequest(new { message = "sorry the email is already used" });
            }
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            return Ok(new { message="User addedd successfully"});
        }

        // sotre in local storege
        //private string CreateToken(User user)
        //{
        //    var claims = new List<Claim>
        //    {
        //         new Claim(JwtRegisteredClaimNames.NameId,user.UserName),
        //         new Claim(JwtRegisteredClaimNames.Email,user.Email)
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8
        //        .GetBytes(_config.GetSection("secret_Key").Value));
        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Audience ="localhost",
        //        Issuer="localhost",
        //        Expires=DateTime.Now.AddDays(1),
        //        SigningCredentials=creds
        //    };
        //    var tokenhandler = new JwtSecurityTokenHandler();

        //    var token = tokenhandler.CreateToken(tokenDescriptor);
        //    return tokenhandler.WriteToken(token);
        //}
    }
}
