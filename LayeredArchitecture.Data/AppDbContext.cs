using LayeredArchitecture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LayeredArchitecture.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define static IDs for consistency
            var adminRoleId = "admin-role-id";
            var adminUserId = "admin-user-id";

            // Seed Admin Role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            // Seed Admin User with static values
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEJ1tB8YVvjHnzIElqPawm35VX8grA3uUZ6xzZXSCA+rLQB4A0VdpX4BNoYoM1G0vnw==",
                SecurityStamp = "7F8C9D6E-2B3A-4C5D-8E7F-9A1B2C3D4E5F",
                ConcurrencyStamp = "1A2B3C4D-5E6F-7A8B-9C0D-1E2F3A4B5C6D"
            };

            // Seed User
            builder.Entity<ApplicationUser>().HasData(adminUser);

            // Link Admin User to Admin Role
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }
    }
}