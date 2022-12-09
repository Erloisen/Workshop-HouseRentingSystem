namespace HouseRentingSystem.Web.Controllers
{
    using System.Collections.Generic;

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
        private readonly IHouseService houseService;
        private readonly IAgentService agentService;

        public HouseController(
            IHouseService houseService,
            IAgentService agentService)
        {
            this.houseService = houseService;
            this.agentService = agentService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel query)
        {
            var queryResult = await this.houseService.All(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllHousesQueryModel.HousesPerPage);

            query.TotalHousesCount = queryResult.TotalHousesCount;
            query.Houses = queryResult.Houses;

            query.Categories = await this.houseService.AllCategoriesNames();

            return this.View(query);
        }

        public async Task<IActionResult> Mine()
        {
            IEnumerable<HouseServiceModel> myHouses;

            var userId = this.User.Id();

            if (await this.agentService.ExistsById(userId))
            {
                var currenAgentId = await this.agentService.GetAgentId(userId);

                myHouses = await this.houseService.AllHousesByAgentId(currenAgentId);
            }
            else
            {
                myHouses = await this.houseService.AllHousesByUserId(userId);
            }

            return this.View(myHouses);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string information)
        {
            if (await this.houseService.Exists(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var model = await this.houseService.HouseDetailsById(id);

            if (information != model.GetInformation())
            {
                this.TempData[MessageConstant.ErrorMessage] = "Don't tuch my slug!";

                return this.RedirectToAction("Index", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await this.agentService.ExistsById(this.User.Id()) == false)
            {
                return this.RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            return this.View(new HouseInputModel()
            {
                HouseCategories = await this.houseService.AllCategories(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.HouseCategories = await this.houseService.AllCategories();

                return this.View(model);
            }

            if (await this.agentService.ExistsById(this.User.Id()) == false)
            {
                return this.RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (await this.houseService.CategoryExists(model.CategoryId) == false)
            {
                this.TempData[MessageConstant.ErrorMessage] = "Category does not exist!";

                return this.View(model);
            }

            var agentId = this.agentService.GetAgentId(this.User.Id()).Result;

            var newHouseId = await this.houseService.Create(model.Title, model.Address, model.Description, model.ImageUrl, model.PricePerMonth, model.CategoryId, agentId);

            return this.RedirectToAction(nameof(this.Details), new { id = newHouseId, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await this.houseService.Exists(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (await this.houseService.HasAgentWithId(id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await this.houseService.HouseDetailsById(id);
            var categoryId = await this.houseService.GetHouseCategoryId(house.Id);

            var houseModel = new HouseInputModel()
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = categoryId,
                HouseCategories = await this.houseService.AllCategories(),
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

            if (await this.houseService.Exists(model.Id) == false)
            {
                this.ModelState.AddModelError(string.Empty, HouseErrorMessage);
                model.HouseCategories = await this.houseService.AllCategories();

                return this.View(model);
            }

            if (await this.houseService.HasAgentWithId(id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await this.houseService.CategoryExists(model.CategoryId) == false)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), CategoryErrorMessage);
                model.HouseCategories = await this.houseService.AllCategories();

                return this.View(model);
            }

            if (this.ModelState.IsValid == false)
            {
                model.HouseCategories = await this.houseService.AllCategories();
            }

            await this.houseService.Edit(model.Id, model);

            return this.RedirectToAction(nameof(this.Details), new { id = model.Id, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await this.houseService.Exists(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (await this.houseService.HasAgentWithId(id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var house = await this.houseService.HouseDetailsById(id);

            var model = new HouseDetailsViewModel()
            {
                Id = house.Id,
                Address = house.Address,
                Title = house.Title,
                ImageUrl = house.ImageUrl,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseDetailsViewModel model)
        {
            if (await this.houseService.Exists(model.Id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (await this.houseService.HasAgentWithId(model.Id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await this.houseService.Delete(model.Id);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if (await this.houseService.Exists(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (await this.agentService.ExistsById(this.User.Id()))
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if (await this.houseService.IsRented(id))
            {
                return this.RedirectToAction(nameof(this.All));
            }

            await this.houseService.Rent(id, this.User.Id());

            return this.RedirectToAction(nameof(this.Mine));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (await this.houseService.Exists(id) == false ||
                await this.houseService.IsRented(id) == false)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (await this.houseService.IsRentedByUserWithId(id, this.User.Id()) == false)
            {
                return this.RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await this.houseService.Leave(id);

            return this.RedirectToAction(nameof(this.Mine));
        }
    }
}
