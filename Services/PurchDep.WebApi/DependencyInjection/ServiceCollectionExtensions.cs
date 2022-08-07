using PurchDep.Dal;
using PurchDep.Dal.InMemory.DependencyInjection;

namespace PurchDep.WebApi.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppContext(this IServiceCollection services, IConfiguration configuration)
        {
            var dbName = configuration["Database"];
            var dbConnectionString = string.Empty;

            if (!dbName.Equals("InMemory"))
                dbConnectionString = configuration.GetConnectionString(dbName);

            switch (dbName)
            {
                case "InMemory":
                    return services.AddInMemoryDatabase("PurchDep.WebApi.InMemoryDatabase").AddScoped<DbInitializer>();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
