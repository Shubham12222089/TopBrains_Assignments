using OrderManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; set; }
        Task<int> SaveChanges();
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public new async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
