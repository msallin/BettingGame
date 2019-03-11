using System.Threading.Tasks;

using BettingGame.Framework.Web.Controller;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.UserProfile;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.UserManagement.Web.Controllers
{
    [Route("api/[controller]"), Authorize]
    public class ProfileController : CqrsControllerBase
    {
        public ProfileController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpGet]
        public async Task<Profile> Get()
        {
            var query = new UserProfileQuery();
            return await QueryPublisher.ExecuteAsync(query);
        }

        [HttpPost]
        public async Task Post([FromBody] UpdateUserProfileCommand command)
        {
            await CommandPublisher.ExecuteAsync(command);
        }
    }
}
