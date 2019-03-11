using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework.Security;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Registration.Abstraction;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.Registration
{
    internal class RegisterUserCommandHandler : ISubscriber
    {
        private readonly IPasswordStorage _passwordStorage;

        private readonly IUserCreator _userCreator;

        public RegisterUserCommandHandler(IUserCreator userCreator, IPasswordStorage passwordStorage)
        {
            _userCreator = userCreator;
            _passwordStorage = passwordStorage;
        }

        [Subscribe]
        public Task ExecuteAsync(RegisterUserCommand command)
        {
            User user = _userCreator.Create();
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Nickname = command.Nickname;
            user.Email = command.Email; // No duplicates allowed. Enforced by an index.
            user.PasswordHash = _passwordStorage.Create(command.Password);
            user.Roles = new List<string> { UserRoles.Participant };
            return _userCreator.SaveAsync(user);
        }
    }
}
