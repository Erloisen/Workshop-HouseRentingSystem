namespace HouseRentingSystem.Web.ViewModels.House
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    using static HouseRentingSystem.Common.GlobalConstants.HouseServiceConstants;

    public class HouseServiceModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string Address { get; init; }

        [Display(Name = DisplayNameImageUrl)]
        public string ImageUrl { get; init; }

        [Display(Name = DisplayNamePricePerMonth)]
        public decimal PricePerMonth { get; init; }

        [Display(Name = DisplayNameIsRented)]
        public bool IsRented { get; init; }
    }
}
