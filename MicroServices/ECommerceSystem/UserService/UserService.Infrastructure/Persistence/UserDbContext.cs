using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Identity;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Persistence
{
    //The third type parameter (Guid) in IdentityDbContext<ApplicationUser,
    //ApplicationRole, Guid>
    //specifies the type of the primary key used by all Identity
    //entities (users, roles, claims, etc.).

    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RefreshToken>(entity =>
            {
                // Configure foreign key explicitly, ensure EF uses the existing UserId column:
                entity.HasOne<ApplicationUser>() // Navigation to ApplicationUser
                .WithMany(u => u.RefreshTokens) // Collection navigation on ApplicationUser
                .HasForeignKey(rt => rt.UserId) // Explicit FK property
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Address>(entity =>
            {
                entity.HasOne<ApplicationUser>() // Navigation to ApplicationUser
                .WithMany(u => u.Addresses) // Collection navigation on ApplicationUser
                .HasForeignKey(a => a.UserId) // Explicit FK property
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            });
            // Rename Identity tables here:
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<ApplicationRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            var adminRoleId = Guid.Parse("c4a3298c-6198-4d12-bd1a-56d1d1ce0aa7");
            var customerRoleId = Guid.Parse("38b657f4-ac20-4a5c-b2a3-16dfad61c381");
            var vendorRoleId = Guid.Parse("582880c3-f554-490f-a24e-526db35cffa5");
            builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                Description = "Administrator with full permissions",
                ConcurrencyStamp = "fe402504-d9a8-4e5c-aa8c-1d0fa9186bcf"
            },
            new ApplicationRole
            {
                Id = customerRoleId,
                Name = "Customer",
                NormalizedName = "CUSTOMER",
                Description = "Customer with shopping permissions",
                ConcurrencyStamp = "5cac671c-23ef-4bf1-a727-5439976d1183"
            },
            new ApplicationRole
            {
                Id = vendorRoleId,
                Name = "Vendor",
                NormalizedName = "VENDOR",
                Description = "Vendor who can manage products",
                ConcurrencyStamp = "ed2aeec6-4b30-4798-b8d3-c41cb84a334c"
            }
            );
            builder.Entity<Client>().HasData(
            new Client { ClientId = "web", ClientName = "Web Client", Description = "Web browser clients", IsActive = true },
            new Client { ClientId = "android", ClientName = "Android Client", Description = "Android mobile app", IsActive = true },
            new Client { ClientId = "ios", ClientName = "iOS Client", Description = "iOS mobile app", IsActive = true }
            );
        }

    }
}
















//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using UserService.Infrastructure.Identity;
//using UserService.Domain.Entities;
//namespace UserService.Infrastructure.Persistence
//{
//    //The third type parameter (Guid) in IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
//    //specifies the type of the primary key used by all Identity entities (users, roles, claims, etc.).
//    public class UserDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
//    {
//        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
//        public DbSet<Address> Addresses { get; set; } = null!;
//        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
//        public DbSet<Client> Clients { get; set; } = null!;
//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);
//            builder.Entity<RefreshToken>(entity =>
//            {
//                // Configure foreign key explicitly, ensure EF uses the existing UserId column:
//                entity.HasOne<ApplicationUser>() // Navigation to ApplicationUser
//                .WithMany(u => u.RefreshTokens) // Collection navigation on ApplicationUser
//                .HasForeignKey(rt => rt.UserId) // Explicit FK property
//                .IsRequired()
//                .OnDelete(DeleteBehavior.Cascade);
//            });
//            builder.Entity<Address>(entity =>
//            {
//                entity.HasOne<ApplicationUser>() // Navigation to ApplicationUser
//                .WithMany(u => u.Addresses) // Collection navigation on ApplicationUser
//                .HasForeignKey(a => a.UserId) // Explicit FK property
//                .IsRequired()
//                .OnDelete(DeleteBehavior.Cascade);
//            });
//            // Rename Identity tables here:
//            builder.Entity<ApplicationUser>().ToTable("Users");
//            builder.Entity<ApplicationRole>().ToTable("Roles");
//            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
//            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
//            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
//            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
//            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
//            var adminRoleId = Guid.Parse("c4a3298c-6198-4d12-bd1a-56d1d1ce0aa7");
//            var customerRoleId = Guid.Parse("38b657f4-ac20-4a5c-b2a3-16dfad61c381");
//            var vendorRoleId = Guid.Parse("582880c3-f554-490f-a24e-526db35cffa5");
//            builder.Entity<ApplicationRole>().HasData(
//            new ApplicationRole
//            {
//                Id = adminRoleId,
//                Name = "Admin",
//                NormalizedName = "ADMIN",
//                Description = "Administrator with full permissions",
//                ConcurrencyStamp = "fe402504-d9a8-4e5c-aa8c-1d0fa9186bcf"
//            },
//            new ApplicationRole
//            {
//                Id = customerRoleId,
//                Name = "Customer",
//                NormalizedName = "CUSTOMER",
//                Description = "Customer with shopping permissions",
//                ConcurrencyStamp = "5cac671c-23ef-4bf1-a727-5439976d1183"
//            },
//            new ApplicationRole
//            {
//                Id = vendorRoleId,
//                Name = "Vendor",
//                NormalizedName = "VENDOR",
//                Description = "Vendor who can manage products",
//                ConcurrencyStamp = "ed2aeec6-4b30-4798-b8d3-c41cb84a334c"
//            }
//            );
//            builder.Entity<Client>().HasData(
//            new Client { ClientId = "web", ClientName = "Web Client", Description = "Web browser clients", IsActive = true },
//            new Client { ClientId = "android", ClientName = "Android Client", Description = "Android mobile app", IsActive = true },
//            new Client { ClientId = "ios", ClientName = "iOS Client", Description = "iOS mobile app", IsActive = true }
//            );
//        }
//    }
//}
