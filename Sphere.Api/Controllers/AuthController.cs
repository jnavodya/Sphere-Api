using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sphere.Api.Models.Request;
using Sphere.Application.Interfaces;
using Sphere.Application.Models;
using System.Net;

namespace Sphere.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Username and password are required.");
            }

            UserModel user = new UserModel
            {
                Username = model.Username,
                Password = model.Password
            };

            var token = _authService.Authenticate(user);

            if (token == "Unauthorized")
            {
                return Unauthorized("Incorrect password.");
            }

            return Ok(new { Token = token });
        }
    }
}
