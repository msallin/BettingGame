using System;
using BettingGame.Betting.Core.Features.ExportBetResults;
using BettingGame.Betting.Core.Features.GameHandling;
using BettingGame.Betting.Core.Features.ParticipantBet;
using BettingGame.Betting.Core.Features.TeamHandling;
using BettingGame.Betting.Persistence.Read;
using BettingGame.Betting.Persistence.Write;
using BettingGame.Betting.Web.IoC;
using BettingGame.Betting.Web.Scheduler;
using BettingGame.Framework.MongoDb;
using BettingGame.Framework.Web.ExceptionHandling;
using BettingGame.Framework.Web.Security;
using Confluent.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silverback.Messaging;
using Silverback.Messaging.Broker;
using Silverback.Messaging.Configuration;
using Silverback.Messaging.Messages;
using Silverback.Messaging.Publishing;

namespace BettingGame.Betting.Web
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Betting API V1");
                c.RoutePrefix = string.Empty;
            });

            busConfigurator.Connect(endpoints => endpoints.AddInbound(new KafkaConsumerEndpoint("team_event")
            {
                Configuration = new KafkaConsumerConfig
                {
                    BootstrapServers = "PLAINTEXT://kafka:9092",
                    GroupId = "Betting",
                    AutoOffsetReset = AutoOffsetResetType.Earliest
                }
            }));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.AddExceptionHandling());

            // Register Web Framework dependencies
            services.AddBettingGameJwtAuthentication(Configuration);
            services.AddBettingGameSwaggerGen("Betting API");

            // Register the Web dependencies
            services.AddBettingWeb();

            // Register Features
            services.AddFeatureGameHandling<BetCommandRepository, GameMetadataCommandRepository>();
            services.AddFeatureTeamHandling<BetCommandRepository, TeamMetadataCommandRepository>();
            services.AddFeatureParticipantBet<HttpContextPrincipalProvider, BetCommandRepository, GameMetadataCommandRepository, BetReader>();
            services.AddFeatureExportBetResults<BetReader>();

            services
                .AddBus(options => options.UseModel())
                .AddBroker<KafkaBroker>(options => options.AddInboundConnector());

            services.AddMongoDbPersistance(Configuration);
        }

        private static void ScheduleTaskRegistrer(IServiceCollection service, Func<ICommand> commandFactory, TimeSpan delay) => service.AddSingleton(provider => (IHostedService)new GenericHostedService(provider.GetRequiredService<ICommandPublisher>(), commandFactory, delay));
    }
}
