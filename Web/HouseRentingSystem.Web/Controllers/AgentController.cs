namespace HouseRentingSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using HouseRentingSystem.Common;
    using HouseRentingSystem.Common.Extensions;
    using HouseRentingSystem.Services.Data;
    using HouseRentingSystem.Web.ViewModels.Agent;
    using Microsoft.AspNetCore.Mvc;

    public class AgentController : BaseController
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await this.agentService.ExistsById(this.User.Id()))
            {
                this.TempData[MessageConstant.ErrorMessage] = "You are already an Agent!";

                return this.RedirectToAction("Index", "Home");
            }

            var model = new BecomeAgentModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentModel model)
        {
            var userId = this.User.Id();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (await this.agentService.ExistsById(userId))
            {
                this.TempData[MessageConstant.ErrorMessage] = "You are already an Agent!";

                return this.RedirectToAction("Index", "Home");
            }

            if (await this.agentService.UserWithPhoneNumberExists(model.PhoneNumber))
            {
                this.TempData[MessageConstant.ErrorMessage] = "Phone number already exists. Enter another one.";

                return this.RedirectToAction("Index", "Home");
            }

            if (await this.agentService.UserHasRent(userId))
            {
                this.TempData[MessageConstant.ErrorMessage] = "You should have no rents to become an agent!";

                return this.RedirectToAction("Index", "Home");
            }

            await this.agentService.Create(userId, model.PhoneNumber);

            return this.RedirectToAction("All", "House");
        }
    }
}
