namespace HouseRentingSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Common.Repositories;
    using HouseRentingSystem.Data.Models;
    using HouseRentingSystem.Web.ViewModels.House;
    using Microsoft.EntityFrameworkCore;

    public class HouseService : IHouseService
    {
        private readonly IDeletableEntityRepository<House> houseRepo;
        private readonly IDeletableEntityRepository<Category> categoryRepo;

        public HouseService(
            IDeletableEntityRepository<House> houseRepo,
            IDeletableEntityRepository<Category> categoryRepo)
        {
            this.houseRepo = houseRepo;
            this.categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<HouseCategoryModel>> AllCategories()
        {
            return await this.categoryRepo.All()
                    .Select(h => new HouseCategoryModel()
                    {
                        Id = h.Id,
                        Name = h.Name,
                    })
                    .ToListAsync();
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await this.categoryRepo.All()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, int agentId)
        {
            var house = new House()
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price,
                CategoryId = categoryId,
                AgentId = agentId,
            };

            await this.houseRepo.AddAsync(house);
            await this.houseRepo.SaveChangesAsync();

            return house.Id;
        }

        public async Task<IEnumerable<HouseIndexModel>> LastThreeHouses()
        {
            return await this.houseRepo.All()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexModel
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title,
                })
                .Take(3)
                .ToListAsync();
        }
    }
}
