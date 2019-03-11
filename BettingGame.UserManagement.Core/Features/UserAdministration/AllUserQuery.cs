using System.Collections.Generic;

using BettingGame.UserManagement.Core.Domain;

using Silverback.Messaging.Messages;

namespace BettingGame.UserManagement.Core.Features.UserAdministration
{
    public class AllUserQuery : IQuery<IEnumerable<User>>
    { }
}
