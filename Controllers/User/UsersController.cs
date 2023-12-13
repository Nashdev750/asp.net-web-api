using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Intacct.Models;
using BCrypt;
using NuGet.Versioning;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Diagnostics;
// using JWT

namespace backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IntacctContext _context;

        public UserController(IntacctContext context)
        {
            _context = context;
        }

        // // GET: api/User
        // [HttpGet]
        // [Authorize]
        // public async Task<IActionResult> GetUser()
        // {
        //   var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        //   return Ok($"User ID: {userIdClaim.Value}");
        // }
        // POST: api/register
        [HttpPost("register")]
        public  async Task<IActionResult> RegisterUser(User user)
        {
            try
            {
                var users =  _context.users.Where(usr=>usr.Email==user.Email).FirstOrDefault();
                if(users !=null) return BadRequest(new Dictionary<string,string>{{"error","Email already in use"}});
                var password =  BCrypt.Net.BCrypt.HashPassword(user?.Password);
                var newUser = new User {
                FullName = user?.FullName,
                Email = user?.Email,
                Password = password
                };
                _context.users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(newUser); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: "+ex.Message);
            }
          
        }
        // GET: api/register
        [HttpPost("login")]
        public  async Task<IActionResult> LoginUser(string email, string password)
        {
            try
            {
                var user =  _context.users.Where(usr=>usr.Email==email).FirstOrDefault();
                if(user == null) return BadRequest(new Dictionary<string,string>{{"error","Email or password is incorrect"}});
                var check = BCrypt.Net.BCrypt.Verify(password, user?.Password);
                if(!check) return BadRequest(new Dictionary<string,string>{{"error","Email or password is incorrect"}});
                
                JWT jwt  = new JWT();
                var token = jwt.getToken(user!);
                var userData = new UserAuth {
                    Id = user!.Id,
                    Email = user.Email,
                    Token = token

                };
                

                return Ok(userData); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error: "+ex.Message);
            }
          
        }

    }
}
