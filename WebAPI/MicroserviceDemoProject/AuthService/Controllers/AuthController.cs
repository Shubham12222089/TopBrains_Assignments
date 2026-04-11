using Microsoft.AspNetCore.Mvc;
using AuthService.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login(string username)
    {
        var token = _jwtService.GenerateToken(username);
        return Ok(new { token });
    }
}