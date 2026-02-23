using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCodeFirstApproach.Model.Context
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; } 
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=SchoolDB;
          Trusted_Connection=True;TrustServerCertificate=True");
        }

    }
}
