using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;
using BettingGame.Tournament.Core.Domain;
using BettingGame.Tournament.Core.Features.TeamAdministration;
using BettingGame.Tournament.Core.Features.TeamOverview;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Tournament.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class TeamController : CqrsControllerBase
    {
        private readonly IPublisher _publisher;

        public TeamController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher, IPublisher publisher)
            : base(queryPublisher, commandPublisher)
        {
            _publisher = publisher;
        }

        [HttpDelete]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Delete([FromBody] DeleteTeamCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }

        [HttpGet]
        public async Task<IEnumerable<Team>> Get()
        {
            return await QueryPublisher.ExecuteAsync(new AllTeamsQuery());
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Post([FromBody] CreateTeamCommand command)
        {
            await _publisher.PublishAsync(command);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Put([FromBody] UpdateTeamCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
