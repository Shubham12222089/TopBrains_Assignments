using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "Clean Code", Author = "Robert C. Martin", Price = 599.99m },
                new Book { BookId = 2, Title = "Design Patterns", Author = "GoF", Price = 749.99m },
                new Book { BookId = 3, Title = "Refactoring", Author = "Martin Fowler", Price = 649.99m }
            );
        }
    }
}
