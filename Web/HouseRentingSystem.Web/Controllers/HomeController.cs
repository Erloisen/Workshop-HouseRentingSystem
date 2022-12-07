namespace HouseRentingSystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using HouseRentingSystem.Services.Data;
    using HouseRentingSystem.Web.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IHouseService houseService;

        public HomeController(IHouseService houseService)
        {
            this.houseService = houseService;
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
            var model = await this.houseService.LastThreeHouses();

            return this.View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
