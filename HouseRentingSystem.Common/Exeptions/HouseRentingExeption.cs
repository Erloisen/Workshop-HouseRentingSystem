namespace HouseRentingSystem.Common.Exeptions
{
    using System;
    using System.Threading;

    public class HouseRentingExeption : ApplicationException
    {
        public HouseRentingExeption()
        {
        }

        public HouseRentingExeption(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
