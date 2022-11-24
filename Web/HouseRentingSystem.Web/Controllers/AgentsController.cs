namespace HouseRentingSystem.Web.Controllers
{
	using HouseRentingSystem.Web.ViewModels.Agent;
	using Microsoft.AspNetCore.Mvc;

	public class AgentsController : BaseController
	{
		[HttpGet]
		public IActionResult Become() => View();

        public IActionResult Become(BecomeAgentModel agent)
		{
			return RedirectToAction(nameof(HouseController.All), "Houses");
		}
	}
}
