using BettingGame.Framework.Clients.Betting;
using BettingGame.Framework.MongoDb;
using BettingGame.Framework.Web.ExceptionHandling;
using BettingGame.Framework.Web.Security;
using BettingGame.Tournament.Core.Features.GameAdministration;
using BettingGame.Tournament.Core.Features.GameOverview;
using BettingGame.Tournament.Core.Features.NotifyBettingAboutGame;
using BettingGame.Tournament.Core.Features.NotifyBettingAboutResult;
using BettingGame.Tournament.Core.Features.ResultAdministration;
using BettingGame.Tournament.Core.Features.TeamAdministration;
using BettingGame.Tournament.Core.Features.TeamOverview;
using BettingGame.Tournament.Persistence.Read;
using BettingGame.Tournament.Persistence.Write;
using BettingGame.Tournament.Web.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Messaging;
using Silverback.Messaging.Broker;
using Silverback.Messaging.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

using BettingGame.DomainEvents;

namespace BettingGame.Tournament.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BusConfigurator busConfigurator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors(builder =>
            {
                builder.WithOrigins("*").AllowAnyHeader().WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tournament API V1");
                c.RoutePrefix = string.Empty;
            });

            busConfigurator.Connect(endpoints => endpoints.AddOutbound<TeamChangedEvent>(new KafkaProducerEndpoint("team_event")
            {
                Configuration = new KafkaProducerConfig
                {
                    BootstrapServers = "PLAINTEXT://kafka:9092"
                }
            }));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<BettingClientConfiguration>(Configuration); // Necessary to make the BettingClient dependency work.

            services.AddMvc(options => options.AddExceptionHandling());

            // Register Web Framework dependencies
            services.AddBettingGameJwtAuthentication(Configuration);
            services.AddBettingGameSwaggerGen("Tournament API");

            services
                .AddBus(options => options.UseModel())
                .AddBroker<KafkaBroker>(options => options.AddOutboundConnector());

            // Register the Web dependencies
            services.AddTournamentWeb();

            // Register Features
            services.AddFeatureTeamAdministration<TeamCommandRepository>();
            services.AddFeatureGameAdministration<GameCommandRepository>();
            services.AddFeatureTeamOverview<TeamReader>();
            services.AddFeatureGameOverview<GameReader>();
            services.AddFeatureResultAdministration<GameResultCommandRepository>();
            services.AddFeatureNotifyBettingAboutResult<BettingClient, HttpContextPrincipalProvider>();
            services.AddFeatureNotifyBettingAboutGameChange<BettingClient, HttpContextPrincipalProvider>();

            // Register Persistence depenendencies
            services.AddMongoDbPersistance(Configuration);
        }
    }
}
