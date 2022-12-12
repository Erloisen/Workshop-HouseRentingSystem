namespace HouseRentingSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using static HouseRentingSystem.Common.GlobalConstants.AdminConstants;

    public class SeedAdministrator : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Users.AnyAsync(a => a.Email == AdministratorEmail))
            {
                return;
            }

            var adminRoll = await dbContext.Roles.FirstOrDefaultAsync();

            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser()
            {
                Email = AdministratorEmail,
                NormalizedEmail = AdministratorEmail.ToUpper(),
                UserName = AdministratorEmail,
                NormalizedUserName = AdministratorEmail.ToUpper(),
                FirstName = "Great",
                LastName = "Admin",
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");

            await dbContext.Users.AddAsync(adminUser);

            await dbContext.Agents.AddAsync(new Agent()
            {
                PhoneNumber = "+359123456789",
                UserId = adminUser.Id,
            });

            await dbContext.SaveChangesAsync();
            await AddRolleToAdmin(dbContext, serviceProvider, adminRoll, adminUser);
        }

        private static async Task AddRolleToAdmin(ApplicationDbContext dbContext, IServiceProvider serviceProvider, ApplicationRole adminRoll, ApplicationUser adminUser)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await userManager.AddToRoleAsync(adminUser, adminRoll.Name);

            await dbContext.SaveChangesAsync();
        }
    }
}
