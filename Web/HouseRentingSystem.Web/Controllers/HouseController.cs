namespace HouseRentingSystem.Web.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    using HouseRentingSystem.Common;
    using HouseRentingSystem.Common.Extensions;
    using HouseRentingSystem.Services.Data;
    using HouseRentingSystem.Web.ViewModels.House;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static HouseRentingSystem.Common.MessageConstant;

    public class HouseController : BaseController
    {
        private readonly IHouseService houses;
        private readonly IAgentService agents;

        public HouseController(
            IHouseService houseService,
            IAgentService agentService)
        {
            this.houses = houseService;
            this.agents = agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = await this.houses.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            query.Categories = await this.houses.AllCategoriesNames();

            return this.View(query);
        }

        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses;

            var userId = this.User.Id();

            if (await this.agents.ExistsById(userId))
            {
                var currenAgentId = await this.agents.GetAgentId(userId);

                myHouses = await this.houses.AllHousesByAgentId(currenAgentId);
            }
            else
            {
                myHouses = await this.houses.AllHousesByUserId(userId);
            }

            return this.View(myHouses);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (await this.houses.Exists(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.houses.HouseDetailsById(id);

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await this.agents.ExistsById(this.User.Id()) == false)
            {
                return this.RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            return this.View(new HouseInputModel()
            {
                HouseCategories = await this.houses.AllCategories(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.HouseCategories = await this.houses.AllCategories();

                return this.View(model);
            }

            if (await this.agents.ExistsById(this.User.Id()) == false)
            {
                return this.RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (await this.houses.CategoryExists(model.CategoryId) == false)
            {
                this.TempData[MessageConstant.ErrorMessage] = "Category does not exist!";

                return this.View(model);
            }

            var agentId = this.agents.GetAgentId(this.User.Id()).Result;

            var newHouseId = await this.houses.Create(model.Title, model.Address, model.Description, model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId);

            return this.RedirectToAction(nameof(this.Details), new { id = newHouseId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await this.houses.Exists(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (await this.houses.HasAgentWithId(id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await this.houses.HouseDetailsById(id);
            var categoryId = await this.houses.GetHouseCategoryId(house.Id);

            var houseModel = new HouseInputModel()
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = categoryId,
                HouseCategories = await this.houses.AllCategories(),
            };

            return this.View(houseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseInputModel model)
        {
            if (id != model.Id)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await this.houses.Exists(model.Id) == false)
            {
                this.ModelState.AddModelError(string.Empty, HouseErrorMessage);
                model.HouseCategories = await this.houses.AllCategories();

                return this.View(model);
            }

            if (await this.houses.HasAgentWithId(id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await this.houses.CategoryExists(model.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), CategoryErrorMessage);
                model.HouseCategories = await this.houses.AllCategories();

                return this.View(model);
            }

            if (this.ModelState.IsValid == false)
            {
                model.HouseCategories = await this.houses.AllCategories();
            }

            await this.houses.Edit(model.Id, model);

            return this.RedirectToAction(nameof(this.Details), new { model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            return this.RedirectToAction(nameof(this.Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            return this.RedirectToAction(nameof(this.Mine));
        }
    }
}
