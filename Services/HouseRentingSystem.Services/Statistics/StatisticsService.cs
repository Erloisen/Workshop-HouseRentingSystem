namespace HouseRentingSystem.Services.Statistics
{
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Common.Repositories;
    using HouseRentingSystem.Data.Models;
    using HouseRentingSystem.Data.Models.Statistics;
    using Microsoft.EntityFrameworkCore;

    public class StatisticsService : IStatisticsService
    {
        private readonly IDeletableEntityRepository<House> houseRepo;

        public StatisticsService(IDeletableEntityRepository<House> houseRepo)
        {
            this.houseRepo = houseRepo;
        }

        public async Task<StatisticsServiceModel> Total()
        {
            var totalHouses = await this.houseRepo.AllAsNoTracking()
                .CountAsync();
            var rentedHouses = await this.houseRepo.AllAsNoTracking()
                .CountAsync(h => h.RenterId != null);

            return new StatisticsServiceModel()
            {
                TotalHouses = totalHouses,
                TotalRent = rentedHouses,
            };
        }
    }
}
