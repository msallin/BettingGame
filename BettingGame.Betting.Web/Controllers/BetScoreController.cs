using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Betting.Core.Features.ExportBetResults;
using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Betting.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Administrator)]
    public class BetScoreController : CqrsControllerBase
    {
        public BetScoreController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpGet]
        public async Task<IEnumerable<BetsWithResultQueryResult>> Get()
        {
            return await QueryPublisher.ExecuteAsync(new BetsWithResultQuery());
        }
    }
}
