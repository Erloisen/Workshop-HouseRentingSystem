namespace HouseRentingSystem.Common.Exeptions
{
    using System;

    public class Guard : IGuard
    {
        public void AgainstNull<T>(T value, string errorMessage = null)
        {
            if (value == null)
            {
                throw new HouseRentingExeption(errorMessage ?? string.Empty);
            }
        }
    }
}
