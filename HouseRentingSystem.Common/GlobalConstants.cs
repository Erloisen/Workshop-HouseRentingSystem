namespace HouseRentingSystem.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "House Renting System";

        public const string AppTitleInHeadTag = "Best House Renting";

        public const string NameAppInHeader = "House Renting Worldwide";

        public const string AdministratorRoleName = "Administrator";

        public static class HouseModelConstants
        {
            public const int TitleMaxLength = 50;

            public const int AddressMaxLength = 150;

            public const int DescriptionMaxLength = 500;

            public const string TypePricePerMonth = "money";

            public const int PriceMinValue = 0;

            public const int PriceMaxValue = 2000;
        }

        public static class CategoryConstants
        {
            public const int NameMaxLength = 50;
        }

        public static class Agent
        {
            public const int PhoneNumberMinLength = 7;

            public const int PhoneNumberMaxLength = 15;

            public const string DisplayNamePhoneNumber = "Phone number";
        }
    }
}
