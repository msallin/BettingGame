using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;
using BettingGame.UserManagement.Core.Features.SignIn.Abstraction;

using Silverback.Messaging.Subscribers;

namespace BettingGame.UserManagement.Core.Features.SignIn
{
    internal class SignInValidQueryHandler : ISubscriber
    {
        private readonly IPasswordStorage _passwordStorage;

        private readonly ISecurityTokenFactory _securityTokenFactory;

        private readonly IUserReader _userReader;

        public SignInValidQueryHandler(IUserReader userReader, ISecurityTokenFactory securityTokenFactory, IPasswordStorage passwordStorage)
        {
            _securityTokenFactory = securityTokenFactory;
            _userReader = userReader;
            _passwordStorage = passwordStorage;
        }

        [Subscribe]
        public async Task<string> ExecuteAsync(SignInValidQuery query)
        {
            User user = await _userReader.ByEmail(query.Email);
            if (user == null)
            {
                throw new ValidationException("User not found!");
            }

            bool isValid = _passwordStorage.Match(query.Password, user.PasswordHash);
            if (isValid)
            {
                return _securityTokenFactory.Create(user);
            }

            throw new ValidationException("Password invalid!");
        }
    }
}
