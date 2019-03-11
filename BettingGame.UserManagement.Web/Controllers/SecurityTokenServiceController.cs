using System.Threading.Tasks;

using BettingGame.Framework.Web.Controller;
using BettingGame.UserManagement.Core.Features.SignIn;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.UserManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class SecurityTokenServiceController : CqrsControllerBase
    {
        public SecurityTokenServiceController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] SignInValidQuery query)
        {
            return Ok(await QueryPublisher.ExecuteAsync(query));
        }
    }
}
