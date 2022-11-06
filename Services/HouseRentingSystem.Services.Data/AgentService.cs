namespace HouseRentingSystem.Services.Data
{
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Common.Repositories;
    using HouseRentingSystem.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class AgentService : IAgentService
    {
        private readonly IDeletableEntityRepository<Agent> agentRepo;
        private readonly IDeletableEntityRepository<House> houseRepo;

        public AgentService(
            IDeletableEntityRepository<Agent> agent,
            IDeletableEntityRepository<House> house)
        {
            this.agentRepo = agent;
            this.houseRepo = house;
        }

        public async Task Create(string userId, string phoneNumber)
        {
            var agent = new Agent()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
            };

            await this.agentRepo.AddAsync(agent);
            await this.agentRepo.SaveChangesAsync();
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await this.agentRepo.All()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> UserHasRent(string userId)
        {
            return await this.houseRepo.All()
                .AllAsync(h => h.RenterId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
        {
            return await this.agentRepo.All()
                .AllAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
