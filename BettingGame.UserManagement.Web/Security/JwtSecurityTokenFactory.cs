using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BettingGame.Framework.Options;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.SignIn.Abstraction;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BettingGame.UserManagement.Web.Security
{
    public class JwtSecurityTokenFactory : ISecurityTokenFactory
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        public JwtSecurityTokenFactory(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string Create(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            IEnumerable<Claim> claims = GetClaims(user);

            var securityToken = new JwtSecurityToken(_jwtOptions.Value.Issuer, claims: claims, expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        private static IEnumerable<Claim> GetClaims(User user)
        {
            yield return new Claim(ClaimTypes.NameIdentifier, user.Id.ToString());

            foreach (string role in user.Roles)
            {
                yield return new Claim(ClaimTypes.Role, role);
            }
        }
    }
}
