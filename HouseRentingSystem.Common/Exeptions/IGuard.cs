namespace HouseRentingSystem.Common.Exeptions
{
    public interface IGuard
    {
        void AgainstNull<T>(T value, string errorMessage = null);
    }
}
