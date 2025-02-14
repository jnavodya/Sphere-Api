using System;
using Microsoft.AspNetCore.Mvc;
using Sphere.Application.Interface;
using System.Collections.Generic;
using Sphere.Application.Services;
using Sphere.Domain.DTO;

namespace Sphere.Api.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class CheckInCheckOutController : ControllerBase
    {
        private readonly ITimerService _timerService;

        public CheckInCheckOutController(ITimerService timerService)
        {
            _timerService = timerService;
        }

        [HttpPost("checkin")]
        public IActionResult CheckIn([FromBody] CheckInDto dto)
        {
            try
            {
                if (dto.UserId <= 0)
                {
                    return BadRequest("Invalid user_id");
                }

                var response = _timerService.CheckInUser(dto.UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
