using LoginAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginAuth.AuthenticateLoginRepository
{
    public class AuthenticateLogin : IAuthenticateLogin
    {
            private readonly LoginDbContext _context;
            public AuthenticateLogin(LoginDbContext context)
            {
                _context = context;
            }
            
            public async Task<UserLogin> AuthenticateUser(string username, string password)
            {
                var user = await _context.UserLogins.FirstOrDefaultAsync(x => x.UserName == username && x.Passcode == password);
                return user;
            }
            public async Task<IEnumerable<UserLogin>> getuser()
            {
                return await _context.UserLogins.ToListAsync();
            }
    }
}
