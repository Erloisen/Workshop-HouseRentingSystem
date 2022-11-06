namespace HouseRentingSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class SeedUsers : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com".ToUpper(),
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com".ToUpper(),
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "agent123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com".ToUpper(),
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com".ToUpper(),
            };

            user.PasswordHash =
                hasher.HashPassword(user, "guest123");

            users.Add(user);
            await dbContext.AddRangeAsync(users);
            dbContext.SaveChanges();
        }
    }
}
