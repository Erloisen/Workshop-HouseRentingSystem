namespace HouseRentingSystem.Common.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalsExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
