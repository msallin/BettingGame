using System.Security.Claims;

namespace BettingGame.Framework.Security
{
    public interface IPrincipalProvider
    {
        ClaimsPrincipal Get();

        string GetSecurityToken();
    }
}
