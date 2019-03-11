using System.Threading.Tasks;

using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;
using BettingGame.Tournament.Core.Features.ResultAdministration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Tournament.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Administrator)]
    public class ResultController : CqrsControllerBase
    {
        public ResultController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpPost]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task Post([FromBody] SetGameResultCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
