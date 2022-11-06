namespace HouseRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HouseRentingSystem.Common;
    using HouseRentingSystem.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    public class House : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.HouseModelConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(GlobalConstants.HouseModelConstants.AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(GlobalConstants.HouseModelConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string ImageUrl { get; set; }

        [Required]
        [Column(TypeName = GlobalConstants.HouseModelConstants.TypePricePerMonth)]
        [Range(GlobalConstants.HouseModelConstants.PriceMinValue, GlobalConstants.HouseModelConstants.PriceMaxValue)]
        [Precision(18, 2)]
        public decimal PricePerMonth { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [Required]
        public int AgentId { get; set; }

        [ForeignKey(nameof(AgentId))]
        public virtual Agent Agent { get; set; }

        public string RenterId { get; set; }

        [ForeignKey(nameof(RenterId))]
        public virtual ApplicationUser Renter { get; set; }
    }
}
