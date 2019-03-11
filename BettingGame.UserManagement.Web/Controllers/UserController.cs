using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework.Security;
using BettingGame.Framework.Web.Controller;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.UserAdministration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Silverback.Messaging.Publishing;

namespace BettingGame.UserManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = UserRoles.Administrator)]
    public class UserController : CqrsControllerBase
    {
        public UserController(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
            : base(queryPublisher, commandPublisher)
        { }

        [HttpGet]
        public async Task<IEnumerable<Profile>> Get(Guid? id)
        {
            if (!id.HasValue)
            {
                return await QueryPublisher.ExecuteAsync(new AllUserQuery());
            }

            return new[] { await QueryPublisher.ExecuteAsync(new UserByIdQuery { Id = id.Value }) };
        }
    }
}
