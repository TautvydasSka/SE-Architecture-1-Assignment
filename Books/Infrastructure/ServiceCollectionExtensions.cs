using Microsoft.Extensions.DependencyInjection;
using Infrastructure.DataAccess;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBooksRepository, BooksRepository>();

            return serviceCollection;
        }
    }
}
