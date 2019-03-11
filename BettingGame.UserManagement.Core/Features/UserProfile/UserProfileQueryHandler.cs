using System;
using System.Threading.Tasks;

using BettingGame.Framework.Extensions;
using BettingGame.Framework.Security;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.UserProfile
{
    internal class UserProfileQueryHandler : ISubscriber
    {
        private readonly IPrincipalProvider _principalProvider;

        private readonly IUserReader _userReader;

        public UserProfileQueryHandler(IPrincipalProvider principalProvider, IUserReader userReader)
        {
            _principalProvider = principalProvider;
            _userReader = userReader;
        }

        [Subscribe]
        public async Task<Profile> ExecuteAsync(UserProfileQuery query)
        {
            Guid userId = _principalProvider.Get().GetUserId();
            User result = await _userReader.ByIdAsync(userId);
            var profile = new Profile
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Nickname = result.Nickname,
                IsAdmin = result.Roles?.Contains(UserRoles.Administrator) ?? false
            };
            return profile;
        }
    }
}
