namespace HouseRentingSystem.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "House Renting System";

        public const string AppTitleInHeadTag = "Best House Renting";

        public const string NameAppInHeader = "House Renting Worldwide";

        public static class HouseModelConstants
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AddressMinLength = 30;
            public const int AddressMaxLength = 150;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;

            public const int ImageUrlMaxLength = 200;

            public const string DisplayNameImageUrl = "Image Url";

            public const string TypePricePerMonth = "money";

            public const double PriceMinValue = 0.00;
            public const double PriceMaxValue = 2000;

            public const string DisplayNamePricePerMonth = "Price Per Month";
        }

        public static class CategoryConstants
        {
            public const int NameMaxLength = 50;

            public const string DisplayNameCategory = "Category";
        }

        public static class AgentConstants
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;

            public const string DisplayNamePhoneNumber = "Phone number";
        }

        public static class HouseServiceConstants
        {
            public const string DisplayNameImageUrl = "Image URL";
            public const string DisplayNamePricePerMonth = "Price Per Month";
            public const string DisplayNameIsRented = "Is Rented";
        }

        public static class HousesQueryModelConstants
        {
            public const int ConstantHousesPerPage = 3;

            public const string DisplayNameSearchTerm = "Search by text";
        }

        public static class UserConstants
        {
            public const int UserFirstNameMaxLength = 12;
            public const int UserFirstNameMinLength = 1;

            public const int UserLastNameMaxLength = 15;
            public const int UserLastNameMinLength = 3;
        }

        public static class AdminConstants
        {
            public const string AdministratorRoleName = "Administrator";
            public const string AdministratorEmail = "admin@mail.com";
        }
    }
}
