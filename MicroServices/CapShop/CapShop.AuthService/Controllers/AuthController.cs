using Microsoft.AspNetCore.Mvc;
using AuthService.DTOs;
using AuthServiceService = AuthService.Services.AuthService;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthServiceService _authService;

        public AuthController(AuthServiceService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public IActionResult Register(RegisterDto dto)
        {
            var result = _authService.Register(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var token = _authService.Login(dto);

            if (token == null)
                return Unauthorized("Invalid credentials");

            return Ok(token);
        }
    }
}