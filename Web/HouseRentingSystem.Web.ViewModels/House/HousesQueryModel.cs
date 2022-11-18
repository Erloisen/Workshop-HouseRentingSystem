namespace HouseRentingSystem.Web.ViewModels.House
{
    using System.Collections.Generic;

    public class HousesQueryModel
    {
        public HousesQueryModel()
        {
            this.Houses = new List<HouseServiceModel>();
        }

        public int TotalHousesCount { get; set; }

        public IEnumerable<HouseServiceModel> Houses { get; set; }
    }
}
