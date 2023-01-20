using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;

        }


        [HttpPost("register")]//Post:api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)//here we returned user dto so that we could pass username and token as return more secure
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken!");

            using var hmac = new HMACSHA512();
            // here 'using' keyword is used to use dispose methode of garbagecollection.
            //hmac is takung temporary memory and it can be cleared by dispose mrthod ofthe class,if we use using keyword
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);//telling our entity framework adding a user
            await _context.SaveChangesAsync();

          return new UserDto
          {
            Username=user.UserName,
            Token=_tokenService.CreateToken (user)
          };

        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x =>
             x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);//we are passing the same key used in the current username is used to get the hashing algorithm

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));//created the same hashing code generated for the user name to check if both the usernames are same or not


            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password!");
            }
           return new UserDto
          {
            Username=user.UserName,
            Token=_tokenService.CreateToken (user)
          };

        }
        private async Task<bool> UserExists(string username)
        {
             return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());

            //here x refferes to AppUser ,
            //anyasync=it determines the sequence contain any elements
        }
    }
}