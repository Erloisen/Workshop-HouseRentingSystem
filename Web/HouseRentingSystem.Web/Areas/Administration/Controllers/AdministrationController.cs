namespace HouseRentingSystem.Web.Areas.Administration.Controllers
{
    using HouseRentingSystem.Common;
    using HouseRentingSystem.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdminConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
