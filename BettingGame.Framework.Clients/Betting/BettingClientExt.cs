using System.Net.Http.Headers;

using BettingGame.Framework.Security;

using Microsoft.Extensions.Options;

namespace BettingGame.Framework.Clients.Betting
{
    public partial class BettingClient
    {
        private readonly IPrincipalProvider _principalProvider;

        public BettingClient(IOptions<BettingClientConfiguration> bettingClientOptions, IPrincipalProvider principalProvider)
            : this(bettingClientOptions.Value.BettingBaseUrl)
        {
            _principalProvider = principalProvider;
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            // Use the token of the current user. Is this hacky? I don't know.
            string token = _principalProvider.GetSecurityToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
