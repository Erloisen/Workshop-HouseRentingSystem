namespace HouseRentingSystem.Data.Common.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using HouseRentingSystem.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);

        Task<bool> Any();
    }
}
