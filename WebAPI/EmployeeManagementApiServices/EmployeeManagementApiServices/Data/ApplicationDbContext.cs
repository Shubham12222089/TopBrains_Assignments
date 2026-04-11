using EmployeeManagementApiServices.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApiServices.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}
