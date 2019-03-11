using System.Threading.Tasks;

using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.UserAdministration
{
    public class UserByIdQueryHandler : ISubscriber
    {
        private readonly IUserReader _userReader;

        public UserByIdQueryHandler(IUserReader userReader)
        {
            _userReader = userReader;
        }

        [Subscribe]
        public async Task<Profile> ExecuteAsync(UserByIdQuery query)
        {
            return await _userReader.ByIdAsync(query.Id);
        }
    }
}
