using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBooksService, BooksService>();

            return serviceCollection;
        }
    }
}
