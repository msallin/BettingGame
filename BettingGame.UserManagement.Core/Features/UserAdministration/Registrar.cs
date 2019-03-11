using BettingGame.Framework;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.UserAdministration
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureUserAdministration<TUserReader>(this IServiceCollection services)
            where TUserReader : class, IUserReader
        {
            // Shared
            services.AddSingleton<IUserReader, TUserReader>();

            // QueryHandler
            services.AddScoped<ISubscriber, AllUserQueryHandler>();
            services.AddScoped<ISubscriber, UserByIdQueryHandler>();
            services.AddSingleton<IStartupTask, CreateInitialAdminStartupTask>();

            return services;
        }
    }
}
