namespace HouseRentingSystem.Services.Data
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Common.Repositories;
    using HouseRentingSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepo;

        public UserService(IDeletableEntityRepository<ApplicationUser> user)
        {
            this.userRepo = user;
        }

        public async Task<string> UserFullName(string userId)
        {
            var currentUser = await this.userRepo.AllAsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (string.IsNullOrEmpty(currentUser.FirstName) || string.IsNullOrEmpty(currentUser.LastName))
            {
                return null;
            }

            StringBuilder fullName = new StringBuilder();
            fullName.Append(currentUser.FirstName);
            fullName.Append(" ");
            fullName.Append(currentUser.LastName);

            return fullName.ToString().TrimEnd();
        }
    }
}
