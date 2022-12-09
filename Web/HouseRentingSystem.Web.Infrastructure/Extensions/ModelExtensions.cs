namespace HouseRentingSystem.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using HouseRentingSystem.Data.Models;

    public static class ModelExtensions
    {
        public static string GetInformation(this IHouseModel house)
        {
            StringBuilder info = new StringBuilder();
            info.Append(house.Title.Replace(" ", "-"));
            info.Append("-");
            info.Append(GetAddress(house.Address));

            return info.ToString().TrimEnd();
        }

        private static string GetAddress(string address)
        {
            string result = string
                .Join("-", address.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Take(3));
            return Regex.Replace(result, @"[^a-zA-Z0-9\-]", string.Empty);
        }
    }
}
