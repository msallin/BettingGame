using System;

using BettingGame.UserManagement.Core.Domain;

using Silverback.Messaging.Messages;

namespace BettingGame.UserManagement.Core.Features.UserAdministration
{
    public class UserByIdQuery : IQuery<Profile>
    {
        public Guid Id { get; set; }
    }
}
