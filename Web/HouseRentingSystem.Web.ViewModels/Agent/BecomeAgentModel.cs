namespace HouseRentingSystem.Web.ViewModels.Agent
{
    using System.ComponentModel.DataAnnotations;

    using HouseRentingSystem.Common;

    public class BecomeAgentModel
    {
        [Required]
        [StringLength(GlobalConstants.Agent.PhoneNumberMaxLength, MinimumLength = GlobalConstants.Agent.PhoneNumberMinLength)]
        [Display(Name = GlobalConstants.Agent.DisplayNamePhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
