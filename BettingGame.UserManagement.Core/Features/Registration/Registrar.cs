using BettingGame.UserManagement.Core.Features.Registration.Abstraction;
using BettingGame.UserManagement.Core.Features.Shared;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;
using BettingGame.UserManagement.Core.Features.UserAdministration;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.Registration
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureRegistration<TUserCreator>(this IServiceCollection services)
            where TUserCreator : class, IUserCreator
        {
            // External dependency
            services.AddSingleton<IUserCreator, TUserCreator>();

            // Shared internal dependency
            services.AddSingleton<IPasswordStorage, HmacSha256PasswordStorage>();

            // CommandHandler
            services.AddScoped<ISubscriber, RegisterUserCommandHandler>();

            // QueryHandler
            services.AddScoped<ISubscriber, AllUserQueryHandler>();

            return services;
        }
    }
}
