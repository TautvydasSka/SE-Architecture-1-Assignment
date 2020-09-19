using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection
                .AddDbContext<BooksDbContext>(options => options.UseSqlServer(connectionString));

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            return serviceCollection;
        }
    }
}
