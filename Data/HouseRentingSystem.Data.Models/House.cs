namespace HouseRentingSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HouseRentingSystem.Data.Common.Models;
    using Microsoft.EntityFrameworkCore;

    using static HouseRentingSystem.Common.GlobalConstants.HouseModelConstants;

    public class House : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        [Column(TypeName = TypePricePerMonth)]
        [Range(PriceMinValue, PriceMaxValue)]
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
