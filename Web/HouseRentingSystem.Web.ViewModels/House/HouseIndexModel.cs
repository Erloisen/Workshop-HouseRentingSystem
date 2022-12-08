namespace HouseRentingSystem.Web.ViewModels.House
{
    using HouseRentingSystem.Data.Models;

    public class HouseIndexModel : IHouseModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; init; }
    }
}
