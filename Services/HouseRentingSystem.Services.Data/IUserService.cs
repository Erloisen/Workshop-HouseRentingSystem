namespace HouseRentingSystem.Services.Data
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<string> UserFullName(string userId);
    }
}
