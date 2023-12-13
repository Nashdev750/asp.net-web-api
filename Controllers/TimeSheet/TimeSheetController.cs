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
    [Route("api/timesheet")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly IntacctContext _context;

        public TimeSheetController(IntacctContext context)
        {
            _context = context;
        }
        // create time sheet
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTimeSheet(){
             return Ok();
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
       

    }
}
