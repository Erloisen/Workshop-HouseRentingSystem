namespace HouseRentingSystem.Web.ViewModels.House
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HouseRentingSystem.Data.Models;

    using static HouseRentingSystem.Common.GlobalConstants.HousesQueryModelConstants;

    public class AllHousesQueryModel
    {
        public const int HousesPerPage = ConstantHousesPerPage;

        public AllHousesQueryModel()
        {
            this.CurrentPage = 1;
            this.Houses = new List<HouseServiceModel>();
        }

        public string Category { get; init; }

        [Display(Name = DisplayNameSearchTerm)]
        public string SearchTerm { get; init; }

        public HouseSorting Sorting { get; init; }

        public int CurrentPage { get; init; }

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<HouseServiceModel> Houses { get; set; }
    }
}
