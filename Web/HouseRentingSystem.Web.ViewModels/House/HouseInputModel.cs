namespace HouseRentingSystem.Web.ViewModels.House
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static HouseRentingSystem.Common.GlobalConstants.CategoryConstants;
    using static HouseRentingSystem.Common.GlobalConstants.HouseModelConstants;
    using static HouseRentingSystem.Common.MessageConstant;

    public class HouseInputModel
    {
        public HouseInputModel()
        {
            this.HouseCategories = new List<HouseCategoryModel>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; }

        [Required]
        [Display(Name = DisplayNameImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(PriceMinValue, PriceMaxValue, ErrorMessage = PricePerMonthErrorMessage)]
        [Display(Name = DisplayNamePricePerMonth)]
        public decimal PricePerMonth { get; set; }

        [Display(Name = DisplayNameCategory)]
        public int CategoryId { get; set; }

        public IEnumerable<HouseCategoryModel> HouseCategories { get; set; }
    }
}
