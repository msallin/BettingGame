using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using BettingGame.Framework.Options;
using BettingGame.Framework.Security;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BettingGame.Framework.Web.Security
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddBettingGameJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            var jwtOptions = new JwtOptions();
            configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Add the access_token as a claim, as we may actually need it
                        if (context.SecurityToken is JwtSecurityToken accessToken)
                        {
                            var identity = context.Principal.Identity as ClaimsIdentity;
                            identity?.AddClaim(new Claim(CustomClaimTypes.AccessToken, accessToken.RawData));
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
