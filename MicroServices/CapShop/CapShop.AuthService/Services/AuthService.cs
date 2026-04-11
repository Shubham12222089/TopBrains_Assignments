using AuthService.Data;
using AuthService.DTOs;
using AuthService.Models;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services
{
    public class AuthService
    {
        private readonly AuthDbContext _context;
        private readonly JwtService _jwtService;

        public AuthService(AuthDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public string Register(RegisterDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                Role = "Customer"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return "User Registered";
        }

        public string Login(LoginDto dto)
        {
            var passwordHash = HashPassword(dto.Password);
            var user = _context.Users
                .FirstOrDefault(x => x.Email == dto.Email && x.Password == passwordHash);

            if (user == null)
                return null;

            return _jwtService.GenerateToken(user);
        }

        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}