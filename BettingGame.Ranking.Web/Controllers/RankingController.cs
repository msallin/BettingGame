using System;
using System.Threading.Tasks;

using BettingGame.Framework.Web.Controller;
using BettingGame.Ranking.Core.Domain;
using BettingGame.Ranking.Core.Features.RankingTable;
using BettingGame.Ranking.Core.Features.UserScore;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Ranking.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RankingController : CqrsControllerBase
    {
        public RankingController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpGet]
        public async Task<RankingTableQueryResult> Get()
        {
            return await QueryPublisher.ExecuteAsync(new RankingTableQuery());
        }

        [HttpGet("{userId}")]
        public async Task<RankingEntry> Get(Guid userId)
        {
            return await QueryPublisher.ExecuteAsync(new UserScoreQuery { UserId = userId });
        }
    }
}
