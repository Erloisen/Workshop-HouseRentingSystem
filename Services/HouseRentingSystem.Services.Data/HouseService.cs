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
        private readonly IDeletableEntityRepository<House> repo;

        public HouseService(IDeletableEntityRepository<House> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<HouseIndexModel>> LastThreeHouses()
        {
            return await this.repo.AllAsNoTrackingWithDeleted()
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
