using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;
using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.GameAdministration;
using BettingGame.Tournament.Core.Features.GameOverview;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Tournament.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : CqrsControllerBase
    {
        public GameController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Delete([FromBody] DeleteGameCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Game>> Get(string group, GameType? type, DateTimeOffset? startDate)
        {
            return await QueryPublisher.ExecuteAsync(new GamesQuery(group, type, startDate));
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Post([FromBody] CreateGameCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Put([FromBody] UpdateGameCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
