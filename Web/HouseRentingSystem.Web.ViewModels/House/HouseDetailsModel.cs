namespace HouseRentingSystem.Web.ViewModels.House
{
    using HouseRentingSystem.Web.ViewModels.Agent;

    public class HouseDetailsModel : HouseServiceModel
    {
        public string Description { get; init; }

        public string Category { get; init; }

        public AgentServiceModel Agent { get; init; }
    }
}
