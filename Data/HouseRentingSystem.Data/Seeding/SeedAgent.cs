namespace HouseRentingSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;

    public class SeedAgent : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Agents.Any())
            {
                return;
            }

            await dbContext.Agents.AddAsync(new Agent()
            {
                PhoneNumber = "+359888888888",
                UserId = "dea12856-c198-4129-b3f3-b893d8395082",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
