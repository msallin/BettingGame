using System.Threading.Tasks;

using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;
using BettingGame.Ranking.Core.Features.RefreshRanking;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Ranking.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Administrator)]
    public class TriggerRankingRefreshController : CqrsControllerBase
    {
        public TriggerRankingRefreshController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpPost]
        public async Task Post()
        {
            await CommandPublisher.ExecuteAsync(new RefreshRankingCommand());
        }
    }
}
