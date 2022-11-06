namespace HouseRentingSystem.Web.Controllers
{
    using System.Threading.Tasks;

    using HouseRentingSystem.Web.ViewModels.House;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HouseController : BaseController
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = new HousesQueryModel();

            return this.View(model);
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
        public IActionResult Add() => this.View();

        [HttpPost]
        public async Task<IActionResult> Add(HouseModel model)
        {
            int id = 1;

            return this.RedirectToAction(nameof(this.Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new HouseModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, HouseModel model)
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
