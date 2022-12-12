namespace HouseRentingSystem.Common.Extensions
{
    using System.Security.Claims;

    using static HouseRentingSystem.Common.GlobalConstants.AdminConstants;

    public static class ClaimsPrincipalsExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            var result = user.IsInRole(AdministratorRoleName);
            return result;
        }
    }
}
