namespace HouseRentingSystem.Services.Data
{
    using System.Threading.Tasks;

    public interface IAgentService
    {
        Task<bool> ExistsById(string userId);

        Task<bool> UserWithPhoneNumberExists(string phoneNumber);

        Task<bool> UserHasRent(string userId);

        Task Create(string userId, string phoneNumber);

        Task<int> GetAgentId(string userId);
    }
}
