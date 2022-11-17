namespace HouseRentingSystem.Web.Controllers
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using HouseRentingSystem.Common;
    using HouseRentingSystem.Common.Extensions;
    using HouseRentingSystem.Services.Data;
    using HouseRentingSystem.Web.ViewModels.House;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> All([FromQuery]AllHousesQueryModel query)
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
            var model = new HousesQueryModel();

            return this.View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = new HouseDetailsModel();

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
                Categories = await this.houses.AllCategories(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.houses.AllCategories();

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
            var model = new HouseInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseInputModel model)
        {
            return this.RedirectToAction(nameof(this.Details), new { id });
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
