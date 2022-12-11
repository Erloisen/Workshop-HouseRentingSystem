namespace HouseRentingSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using static HouseRentingSystem.Common.GlobalConstants.AdminConstants;

    public class SeedAdministrator : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Users.FindAsync(AdministratorEmail) != null)
            {
                return;
            }

            var adminRoll = await dbContext.Roles.FirstOrDefaultAsync();

            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser()
            {
                Id = adminRoll.Id,
                Email = AdministratorEmail,
                NormalizedEmail = AdministratorEmail.ToUpper(),
                UserName = AdministratorEmail,
                NormalizedUserName = AdministratorEmail.ToUpper(),
                FirstName = "Great",
                LastName = "Admin",
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");

            await dbContext.Users.AddAsync(adminUser);
            await dbContext.SaveChangesAsync();

            await dbContext.Agents.AddAsync(new Agent()
            {
                PhoneNumber = "+359123456789",
                UserId = adminUser.Id,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
