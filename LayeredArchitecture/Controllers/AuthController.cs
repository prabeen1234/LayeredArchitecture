using LayeredArchitecture.Models;
using LayeredArchitecture.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LayeredArchitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService; 
        public AuthController(AuthService service)
        {
            _authService = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerDto)
        {
            var res = await _authService.RegisterAsync(registerDto.Email,registerDto.Password);
            return Ok(new { Message = res});
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _authService.LoginAsync(model.Email, model.Password);

            if (token == null)
                return Unauthorized(new { Message = "Invalid email or password." });

            return Ok(new { Token = token });
        }

    }
}
