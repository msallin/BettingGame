using System;

using BettingGame.Framework.Clients.Betting;
using BettingGame.Framework.Clients.UserManagement;
using BettingGame.Framework.MongoDb;
using BettingGame.Framework.Web.ExceptionHandling;
using BettingGame.Framework.Web.Security;
using BettingGame.Ranking.Core.Features.RankingTable;
using BettingGame.Ranking.Core.Features.RefreshRanking;
using BettingGame.Ranking.Core.Features.UserScore;
using BettingGame.Ranking.Persistence.Read;
using BettingGame.Ranking.Persistence.Write;
using BettingGame.Ranking.Web.IoC;
using BettingGame.Ranking.Web.Scheduler;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Silverback.Messaging.Broker;
using Silverback.Messaging.Messages;

namespace BettingGame.Ranking.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors(builder => { builder.WithOrigins("*").AllowAnyHeader().WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS"); });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ranking API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BettingClientConfiguration>(Configuration); // Necessary to make the BettingClient dependency work.
            services.Configure<UserManagementClientConfiguration>(Configuration); // Necessary to make the UserManagementClient dependency work.

            services.AddMvc(options => options.AddExceptionHandling());

            // Register Web Framework dependencies
            services.AddBettingGameJwtAuthentication(Configuration);
            services.AddBettingGameSwaggerGen("Ranking API");

            services
                .AddBus(options => options.UseModel())
                .AddBroker<KafkaBroker>(options => options.AddOutboundConnector());

            services.AddMemoryCache();

            // Register the Web dependencies
            services.AddRankingWeb();

            // Register the application features
            services.AddFeatureRankingTable<RankingTableReader>();
            services.AddFeatureUserScore<RankingTableReader>();

            // We know that this feature uses schedule tasks.
            // The task runs outside a request so the SystemUserPrincipalProvider has to be used to provide an identity.
            services.AddFeatureRefreshRanking<BettingClient, UserManagementClient, SystemUserPrincipalProvider, RankingSnapshotCommandRepository>(ScheduleTaskRegistrer);

            services.AddMongoDbPersistance(Configuration);
        }

        private static void ScheduleTaskRegistrer(IServiceCollection service, Func<ICommand> commandFactory, TimeSpan delay)
        {
            service.AddSingleton(provider => (IHostedService)new GenericHostedService(provider, commandFactory, delay));
        }
    }
}
