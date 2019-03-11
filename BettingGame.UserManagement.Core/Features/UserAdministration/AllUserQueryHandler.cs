using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.UserAdministration
{
    public class AllUserQueryHandler : ISubscriber
    {
        private readonly IUserReader _userReader;

        public AllUserQueryHandler(IUserReader userReader)
        {
            _userReader = userReader;
        }

        [Subscribe]
        public Task<IEnumerable<User>> ExecuteAsync(AllUserQuery query)
        {
            return _userReader.AllAsync();
        }
    }
}
