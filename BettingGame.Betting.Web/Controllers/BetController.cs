using System.Threading.Tasks;

using BettingGame.Betting.Core.Features.ParticipantBet;
using BettingGame.Framework.Web.Controller;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Betting.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class BetController : CqrsControllerBase
    {
        public BetController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ParticipantBetQuery query)
        {
            return Ok(await QueryPublisher.ExecuteAsync(query));
        }

        [HttpPost]
        public async Task Post([FromBody] ParticipantBetCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
