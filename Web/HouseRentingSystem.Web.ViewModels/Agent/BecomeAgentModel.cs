namespace HouseRentingSystem.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    using static HouseRentingSystem.Common.GlobalConstants.AgentConstants;

    public class BecomeAgentModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
