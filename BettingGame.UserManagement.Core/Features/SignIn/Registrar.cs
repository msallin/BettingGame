using BettingGame.UserManagement.Core.Features.Shared.Abstraction;
using BettingGame.UserManagement.Core.Features.SignIn.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.SignIn
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureSignIn<TUserReader, TSecurityTokenFactory>(this IServiceCollection services)
            where TUserReader : class, IUserReader
            where TSecurityTokenFactory : class, ISecurityTokenFactory
        {
            // Shared
            services.AddSingleton<IUserReader, TUserReader>();
            services.AddSingleton<ISecurityTokenFactory, TSecurityTokenFactory>();

            // QueryHandler
            services.AddScoped<ISubscriber, SignInValidQueryHandler>();

            return services;
        }
    }
}
