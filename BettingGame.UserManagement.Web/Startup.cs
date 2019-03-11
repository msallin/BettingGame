using System;

using BettingGame.Framework;
using BettingGame.Framework.MongoDb;
using BettingGame.Framework.Web.ExceptionHandling;
using BettingGame.Framework.Web.Security;
using BettingGame.UserManagement.Core.Features.Registration;
using BettingGame.UserManagement.Core.Features.SignIn;
using BettingGame.UserManagement.Core.Features.UserAdministration;
using BettingGame.UserManagement.Core.Features.UserProfile;
using BettingGame.UserManagement.Persistence.Read;
using BettingGame.UserManagement.Persistence.Write;
using BettingGame.UserManagement.Web.IoC;
using BettingGame.UserManagement.Web.Security;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Silverback.Messaging.Broker;

namespace BettingGame.UserManagement.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStarted.Register(OnStarted, app.ApplicationServices);

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserManagement API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.AddExceptionHandling());

            // Register Web Framework dependencies
            services.AddBettingGameJwtAuthentication(Configuration);
            services.AddBettingGameSwaggerGen("UserManagement API");

            // Register the Web dependencies
            services.AddUserManagementWeb();

            // Register the application features
            services.AddFeatureRegistration<UserCreator>();
            services.AddFeatureUserProfile<HttpContextPrincipalProvider, UserReader, UserUpdater>();
            services.AddFeatureSignIn<UserReader, JwtSecurityTokenFactory>();
            services.AddFeatureUserAdministration<UserReader>();

            services
                .AddBus(options => options.UseModel())
                .AddBroker<KafkaBroker>(options => options.AddOutboundConnector());

            services.AddMongoDbPersistance(Configuration);
            services.AddSingleton<IStartupTask, Persistence.MongoDbStartup>();
        }

        private static void OnStarted(object context)
        {
            var provider = (IServiceProvider)context;
            foreach (IStartupTask startupTask in provider.GetServices<IStartupTask>())
            {
                startupTask.Run().Wait();
            }
        }
    }
}
