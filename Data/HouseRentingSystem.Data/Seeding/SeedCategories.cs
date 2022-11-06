namespace HouseRentingSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;

    public class SeedCategories : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Cottage" });
            await dbContext.Categories.AddAsync(new Category { Name = "Single-Family" });
            await dbContext.Categories.AddAsync(new Category { Name = "Duplex" });

            await dbContext.SaveChangesAsync();
        }
    }
}
