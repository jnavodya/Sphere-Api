using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sphere.Api.Models.Request;
using Sphere.Application.Interfaces;
using Sphere.Domain.Enums;
using System.Net;

namespace Sphere.Api.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendenceController(ITimerService timerService) : ControllerBase
    {
        private readonly ITimerService _timerService = timerService;

        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult?> CheckAttendence([FromBody] AttendenceRequest request)
        {
            try
            {
                if (request.Status == AttendanceStatus.CheckIn)
                {
                    var result = await _timerService.CheckIn(request.UserId);
                    return result != null ? Ok(result) : BadRequest(result);
                }
                else if (request.Status == AttendanceStatus.CheckOut)
                {
                    var result = _timerService.CheckOut(request.UserId);
                    return result != null ? Ok(result) : BadRequest(result);
                }

                return BadRequest("Invalid status.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("get/{userId}/{date}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetWorkLogsForDate(int userId, string date)
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate))
            {
                return BadRequest("Invalid date format. Please use YYYY-MM-DD.");
            }

            var workLogs = _timerService.GetWorkLogsForDate(userId, parsedDate);
            if (workLogs.Count == 0)
                return NotFound($"No work logs found for user {userId} on {parsedDate:yyyy-MM-dd}.");

            return Ok(workLogs);
        }

    }
}
