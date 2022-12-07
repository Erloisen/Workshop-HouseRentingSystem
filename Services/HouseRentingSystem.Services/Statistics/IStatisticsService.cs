namespace HouseRentingSystem.Services.Statistics
{
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models.Statistics;

    public interface IStatisticsService
    {
        Task<StatisticsServiceModel> Total();
    }
}
