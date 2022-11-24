namespace HouseRentingSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Runtime.ConstrainedExecution;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Models;
    using HouseRentingSystem.Web.ViewModels.House;

    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexModel>> LastThreeHouses();

        Task<IEnumerable<HouseCategoryModel>> AllCategories();

        Task<bool> CategoryExists(int categoryId);

        Task<int> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, int agentId);

        Task<HousesQueryModel> All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNames();

        Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId);

        Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId);

        Task<bool> Exists(int id);

        Task<HouseDetailsModel> HouseDetailsById(int id);

        Task Edit(int houseId, HouseInputModel model);

        Task<bool> HasAgentWithId(int houseId, string currentUserId);

        Task<int> GetHouseCategoryId(int houseId);
    }
}
