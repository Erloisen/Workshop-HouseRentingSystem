namespace HouseRentingSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HouseRentingSystem.Web.ViewModels.House;

    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<int> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, int agentId);
    }
}
