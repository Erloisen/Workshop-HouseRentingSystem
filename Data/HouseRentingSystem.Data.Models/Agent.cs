namespace HouseRentingSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using HouseRentingSystem.Data.Common.Models;

    using static HouseRentingSystem.Common.GlobalConstants.AgentConstants;

    public class Agent : BaseDeletableModel<int>
    {
        public Agent()
        {
            this.Houses = new HashSet<House>();
        }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<House> Houses { get; set; }
    }
}
