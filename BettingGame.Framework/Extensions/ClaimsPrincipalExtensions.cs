using System;
using System.Security.Claims;

namespace BettingGame.Framework.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            return new Guid(principal.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
