using System;
using System.Threading.Tasks;

using BettingGame.Framework.Extensions;
using BettingGame.Framework.Security;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.UserProfile.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.UserProfile
{
    internal class UpdateUserProfileCommandHandler : ISubscriber
    {
        private readonly IPrincipalProvider _principalProvider;

        private readonly IUserUpdater _userUpdater;

        public UpdateUserProfileCommandHandler(IPrincipalProvider principalProvider, IUserUpdater userUpdater)
        {
            _principalProvider = principalProvider;
            _userUpdater = userUpdater;
        }

        [Subscribe]
        public async Task ExecuteAsync(UpdateUserProfileCommand command)
        {
            Guid userId = _principalProvider.Get().GetUserId();
            User user = await _userUpdater.GetAsync(userId);

            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Email = command.Email;  // No duplicates allowed. Enforced by an index.
            user.Nickname = command.Nickname;

            await _userUpdater.UpdateAsync(user);
        }
    }
}
