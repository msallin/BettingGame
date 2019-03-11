using System.Security.Claims;

using BettingGame.Framework.Security;

using Microsoft.AspNetCore.Http;

namespace BettingGame.Framework.Web.Security
{
    public class HttpContextPrincipalProvider : IPrincipalProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextPrincipalProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal Get()
        {
            return _httpContextAccessor.HttpContext.User;
        }

        public string GetSecurityToken()
        {
            return Get().FindFirstValue(CustomClaimTypes.AccessToken);
        }
    }
}
