using System.Threading.Tasks;

using BettingGame.Framework.Web.Controller;
using BettingGame.UserManagement.Core.Features.Registration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.UserManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegistrationController : CqrsControllerBase
    {
        public RegistrationController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpPost]
        public async Task Post([FromBody] RegisterUserCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
