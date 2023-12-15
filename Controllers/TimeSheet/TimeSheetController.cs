using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
// using JWT

namespace backend.Controllers
{
    [Route("api/timesheet")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
          private readonly TimeSheetService _timeSheetService;


        public TimeSheetController(TimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }
        // create time sheet
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateTimeSheet(){
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var timeSheet = await _timeSheetService.CreateTimeSheet(userId);
                return Ok(timeSheet);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, "Error: "+ex.Message);
            }
            
        }

    }
}
