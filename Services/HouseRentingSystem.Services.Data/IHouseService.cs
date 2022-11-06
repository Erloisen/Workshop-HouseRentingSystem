namespace HouseRentingSystem.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HouseRentingSystem.Web.ViewModels.House;

    public interface IHouseService
    {
        Task<IEnumerable<HouseIndexModel>> LastThreeHouses();
    }
}
