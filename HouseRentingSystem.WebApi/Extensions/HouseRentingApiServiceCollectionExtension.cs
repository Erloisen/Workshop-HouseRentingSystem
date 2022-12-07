namespace Microsoft.Extensions.DependencyInjection
{
    using System.Runtime.CompilerServices;

    using HouseRentingSystem.Common.Exeptions;
    using HouseRentingSystem.Data;
    using HouseRentingSystem.Data.Common.Repositories;
    using HouseRentingSystem.Data.Repositories;
    using HouseRentingSystem.Services.Statistics;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public static class HouseRentingApiServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationSevices(this IServiceCollection services)
        {
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IGuard, Guard>();

            return services;
        }

        public static IServiceCollection AddHouseRentingDbConstext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));

            return services;
        }
    }
}
