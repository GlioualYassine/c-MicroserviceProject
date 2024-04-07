using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogMicroservice.MongoDB
{
    public static class MongoExtension
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            // Read configuration using GetSection
            services.Configure<Mongo>(configuration.GetSection("mongo"));

            // Register MongoClient and IMongoDatabase as singletons
            services.AddSingleton<MongoClient>(provider =>
            {
                var options = provider.GetService<IOptions<Mongo>>();
                return new MongoClient(options.Value.connectionString);
            });

            services.AddSingleton<IMongoDatabase>(provider =>
            {
                var client = provider.GetService<MongoClient>();
                var options = provider.GetService<IOptions<Mongo>>();
                return client.GetDatabase(options.Value.database);
            });

            return services;
        }
    }
}
