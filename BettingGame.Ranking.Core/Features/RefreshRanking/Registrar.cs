using System;

using BettingGame.Framework.Abstraction.Clients.Betting;
using BettingGame.Framework.Abstraction.Clients.UserManagement;
using BettingGame.Framework.Security;
using BettingGame.Ranking.Core.Features.RefreshRanking.Abstraction;

using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Messages;
using Silverback.Messaging.Subscribers;

namespace BettingGame.Ranking.Core.Features.RefreshRanking
{
    public static class Registrar
    {
        public static IServiceCollection AddFeatureRefreshRanking<TBettingClient, TUserManagementClient, TPrincipalProvider, TRankingSnapshotCommandRepository>(this IServiceCollection services, Action<IServiceCollection, Func<ICommand>, TimeSpan> scheduleTaskRegistrer)
            where TBettingClient : class, IBettingClient
            where TUserManagementClient : class, IUserManagementClient
            where TPrincipalProvider : class, IPrincipalProvider
            where TRankingSnapshotCommandRepository : class, IRankingSnapshotCommandRepository
        {
            // External dependency
            scheduleTaskRegistrer(services, () => new RefreshRankingCommand(), TimeSpan.FromMinutes(15));
            services.AddSingleton<IBettingClient, TBettingClient>();
            services.AddSingleton<IUserManagementClient, TUserManagementClient>();
            services.AddSingleton<IPrincipalProvider, TPrincipalProvider>();
            services.AddSingleton<IRankingSnapshotCommandRepository, TRankingSnapshotCommandRepository>();

            // CommandHandler
            services.AddScoped<ISubscriber, RefreshRankingCommandHandler>();

            // EventHandler
            services.AddScoped<ISubscriber, NewRankingSnapshotEventHandler>();

            return services;
        }
    }
}
