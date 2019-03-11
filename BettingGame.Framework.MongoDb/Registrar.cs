using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BettingGame.Framework.MongoDb
{
    public static class Registrar
    {
        public static IServiceCollection AddMongoDbPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Persistence Framework
            services.AddSingleton<DbContextFactory>();

            services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));

            return services;
        }
    }
}
