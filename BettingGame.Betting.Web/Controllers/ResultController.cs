using System.Threading.Tasks;

using BettingGame.Betting.Core.Features.GameHandling;
using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.Betting.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Administrator)]
    public class ResultController : CqrsControllerBase
    {
        public ResultController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpPost]
        public async Task Post([FromBody] SetActualResultCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
