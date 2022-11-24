﻿namespace HouseRentingSystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Common.Repositories;
    using HouseRentingSystem.Data.Models;
    using HouseRentingSystem.Web.ViewModels.Agent;
    using HouseRentingSystem.Web.ViewModels.House;
    using Microsoft.EntityFrameworkCore;

    public class HouseService : IHouseService
    {
        private readonly IDeletableEntityRepository<House> houseRepo;
        private readonly IDeletableEntityRepository<Category> categoryRepo;

        public HouseService(
            IDeletableEntityRepository<House> houseRepo,
            IDeletableEntityRepository<Category> categoryRepo)
        {
            this.houseRepo = houseRepo;
            this.categoryRepo = categoryRepo;
        }

        public async Task<HousesQueryModel> All(string category = null, string searchTerm = null, HouseSorting sorting = HouseSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var result = new HousesQueryModel();
            var housesQuery = this.houseRepo.All();

            if (string.IsNullOrEmpty(category) == false)
            {
                housesQuery = housesQuery
                    .Where(h => h.Category.Name == category);
            }

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                housesQuery = housesQuery
                    .Where(h => EF.Functions.Like(h.Title.ToLower(), searchTerm) ||
                        EF.Functions.Like(h.Address.ToLower(), searchTerm) ||
                        EF.Functions.Like(h.Description.ToLower(), searchTerm));
            }

            housesQuery = sorting switch
            {
                HouseSorting.Price => housesQuery
                    .OrderBy(h => h.PricePerMonth),
                HouseSorting.NotRentedFirst => housesQuery
                    .OrderBy(h => h.RenterId != null)
                    .ThenByDescending(h => h.Id),
                _ => housesQuery.OrderByDescending(h => h.Id),
            };

            result.Houses = await housesQuery
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Select(h => new HouseServiceModel
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    IsRented = h.RenterId != null,
                    PricePerMonth = h.PricePerMonth,
                })
                .ToListAsync();

            result.TotalHousesCount = await housesQuery.CountAsync();

            return result;
        }

        public async Task<IEnumerable<HouseCategoryModel>> AllCategories()
        {
            return await this.categoryRepo.All()
                    .Select(h => new HouseCategoryModel()
                    {
                        Id = h.Id,
                        Name = h.Name,
                    })
                    .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNames()
        {
            return await this.categoryRepo.All()
                .Select(c => c.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByAgentId(int agentId)
        {
            var houses = await this.houseRepo.All()
                .Where(h => h.AgentId == agentId)
                .ToListAsync();

            return this.ProjectToModel(houses);
        }

        public async Task<IEnumerable<HouseServiceModel>> AllHousesByUserId(string userId)
        {
            var houses = await this.houseRepo.All()
                .Where(h => h.RenterId == userId)
                .ToListAsync();

            return this.ProjectToModel(houses);
        }

        public async Task<bool> CategoryExists(int categoryId)
        {
            return await this.categoryRepo.All()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> Create(string title, string address, string description, string imageUrl, decimal price, int categoryId, int agentId)
        {
            var house = new House()
            {
                Title = title,
                Address = address,
                Description = description,
                ImageUrl = imageUrl,
                PricePerMonth = price,
                CategoryId = categoryId,
                AgentId = agentId,
            };

            await this.houseRepo.AddAsync(house);
            await this.houseRepo.SaveChangesAsync();

            return house.Id;
        }

        public async Task Edit(int houseId, HouseInputModel model)
        {
            var house = await this.houseRepo.All()
                .FirstOrDefaultAsync(h => h.Id == houseId);

            house.Title = model.Title;
            house.Address = model.Address;
            house.Description = model.Description;
            house.ImageUrl = model.ImageUrl;
            house.PricePerMonth= model.PricePerMonth;
            house.CategoryId = model.CategoryId;

            await this.houseRepo.SaveChangesAsync();
        }

        public Task Edit(int houseId, HouseModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(int id)
        {
            return await this.houseRepo.All()
                .AnyAsync(h => h.Id == id);
        }

        public async Task<int> GetHouseCategoryId(int houseId)
        {
            var result = await this.houseRepo.AllAsNoTracking()
                .Where(h => h.Id == houseId)
                .FirstOrDefaultAsync();

            return result.CategoryId;
        }

        public async Task<bool> HasAgentWithId(int houseId, string currentUserId)
        {
            bool result = false;
            var house = await this.houseRepo.AllAsNoTracking()
                .Where(h => h.Id == houseId)
                .Include(h => h.Agent)
                .FirstOrDefaultAsync();

            if (house.Agent != null && house.Agent.UserId == currentUserId)
            {
                result = true;
            }

            return result;
        }

        public async Task<HouseDetailsModel> HouseDetailsById(int id)
        {
            return await this.houseRepo.All()
                .Where(h => h.Id == id)
                .Select(h => new HouseDetailsModel()
                {
                    Id = h.Id,
                    Title = h.Title,
                    Address = h.Address,
                    Description = h.Description,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId != null,
                    Category = h.Category.Name,
                    Agent = new AgentServiceModel()
                    {
                        PhoneNumber = h.Agent.PhoneNumber,
                        Email = h.Agent.User.Email,
                    },
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HouseIndexModel>> LastThreeHouses()
        {
            return await this.houseRepo.All()
                .OrderByDescending(h => h.Id)
                .Select(h => new HouseIndexModel
                {
                    Id = h.Id,
                    ImageUrl = h.ImageUrl,
                    Title = h.Title,
                })
                .Take(3)
                .ToListAsync();
        }

        private List<HouseServiceModel> ProjectToModel(List<House> houses)
        {
            return houses
               .Select(h => new HouseServiceModel()
               {
                   Id = h.Id,
                   Title = h.Title,
                   Address = h.Address,
                   ImageUrl = h.ImageUrl,
                   PricePerMonth = h.PricePerMonth,
                   IsRented = h.RenterId != null,
               })
               .ToList();
        }
    }
}
