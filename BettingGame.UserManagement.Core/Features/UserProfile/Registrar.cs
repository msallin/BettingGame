using BettingGame.Framework.Security;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;
using BettingGame.UserManagement.Core.Features.UserProfile.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.UserProfile
{
    public static class Registrar
    {
        public static void AddFeatureUserProfile<TPrincipalProvider, TUserReader, TUserUpdater>(this IServiceCollection services)
            where TUserReader : class, IUserReader
            where TPrincipalProvider : class, IPrincipalProvider
            where TUserUpdater : class, IUserUpdater
        {
            // Shared
            services.AddSingleton<IUserReader, TUserReader>();
            services.AddSingleton<IPrincipalProvider, TPrincipalProvider>();
            services.AddSingleton<IUserUpdater, TUserUpdater>();

            // CommandHandler
            services.AddScoped<ISubscriber, UpdateUserProfileCommandHandler>();

            // QueryHandler
            services.AddScoped<ISubscriber, UserProfileQueryHandler>();
        }
    }
}
