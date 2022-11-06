namespace HouseRentingSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HouseRentingSystem.Common;
    using HouseRentingSystem.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Houses = new HashSet<House>();
        }

        [Required]
        [MaxLength(GlobalConstants.CategoryConstants.NameMaxLength)]
        public string Name { get; set; }

        public virtual IEnumerable<House> Houses { get; set; }
    }
}
