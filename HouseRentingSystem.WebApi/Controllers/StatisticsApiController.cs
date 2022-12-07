namespace HouseRentingSystem.WebApi.Controllers
{
    using HouseRentingSystem.Data.Models.Statistics;
    using HouseRentingSystem.Services.Statistics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/Statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService service;

        public StatisticsApiController(IStatisticsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Get statistics about number of houses and number of rented houses.
        /// </summary>
        /// <returns>Total houses and total rents.</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStatistics()
        {
            var model = await service.Total();

            return Ok(model);
        }
    }
}
