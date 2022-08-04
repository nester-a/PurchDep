using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PurchDep.Dal.InMemory.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryDatabase(this IServiceCollection services, string dbName)
        {
            services.AddDbContext<PurchDepContext>(opt =>
            {
                opt.UseInMemoryDatabase(dbName);
            });

            return services;
        }
    }
}
