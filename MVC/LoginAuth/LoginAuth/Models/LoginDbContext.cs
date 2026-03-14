using Microsoft.EntityFrameworkCore;

namespace LoginAuth.Models
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> options) : base(options)
        {
        }
        public DbSet<UserLogin> UserLogins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("UserLogin");
            });

            builder.Entity<UserLogin>().HasData(
                new UserLogin { Id = 1, UserName = "admin", Passcode = "admin123", isActive = 1 },
                new UserLogin { Id = 2, UserName = "user1", Passcode = "user123", isActive = 1 },
                new UserLogin { Id = 3, UserName = "user2", Passcode = "user456", isActive = 0 }
            );  
            
        }
    }
}
