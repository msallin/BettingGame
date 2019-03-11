using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BettingGame.Framework.Options;
using BettingGame.Framework.Security;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BettingGame.Framework.Web.Security
{
    /// <summary>
    ///     Creates a JWT to enable the system to call anothers system web API.
    /// <remark>
    ///     1. The logic is c&p from the SecurityTokenFactory.
    ///     2. Consider to use at least a own issuer OR get a token for the System from the STS(which might be the best solution).
    /// </remark>
    /// </summary>
    public class SystemUserPrincipalProvider : IPrincipalProvider
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        public SystemUserPrincipalProvider(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string Create()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(_jwtOptions.Value.Issuer, claims: GetClaims(), expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public ClaimsPrincipal Get()
        {
            var claimsIdentity = new ClaimsIdentity(GetClaims(), "Federation");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return claimsPrincipal;
        }

        public string GetSecurityToken()
        {
            return Create();
        }

        private static IEnumerable<Claim> GetClaims()
        {
            yield return new Claim(ClaimTypes.NameIdentifier, Guid.Empty.ToString());
            yield return new Claim(ClaimTypes.Role, UserRoles.Administrator);
        }
    }
}
