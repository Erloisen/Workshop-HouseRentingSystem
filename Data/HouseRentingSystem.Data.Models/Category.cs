namespace HouseRentingSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HouseRentingSystem.Data.Common.Models;

    using static HouseRentingSystem.Common.GlobalConstants.CategoryConstants;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Houses = new HashSet<House>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public virtual IEnumerable<House> Houses { get; set; }
    }
}
