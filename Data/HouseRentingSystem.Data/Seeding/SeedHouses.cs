﻿namespace HouseRentingSystem.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;

    public class SeedHouses : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Houses.Any())
            {
                return;
            }

            await dbContext.Houses.AddAsync(new House()
            {
                Title = "Big House Marina",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://www.luxury-architecture.net/wp-content/uploads/2017/12/1513217889-7597-FAIRWAYS-010.jpg",
                PricePerMonth = 2100.00M,
                CategoryId = 3,
                AgentId = 1,
                RenterId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
            });

            await dbContext.Houses.AddAsync(new House()
            {
                Title = "Family House Comfort",
                Address = "Near the Sea Garden in Burgas, Bulgaria",
                Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                PricePerMonth = 1200.00M,
                CategoryId = 2,
                AgentId = 1,
            });

            await dbContext.Houses.AddAsync(new House()
            {
                Title = "Grand House",
                Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                Description = "This luxurious house is everything you will need. It is just excellent.",
                ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                PricePerMonth = 2000.00M,
                CategoryId = 2,
                AgentId = 1,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
