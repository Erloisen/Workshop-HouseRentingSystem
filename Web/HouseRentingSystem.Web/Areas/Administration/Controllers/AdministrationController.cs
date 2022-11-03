namespace HouseRentingSystem.Web.Areas.Administration.Controllers
{
    using HouseRentingSystem.Common;
    using HouseRentingSystem.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
