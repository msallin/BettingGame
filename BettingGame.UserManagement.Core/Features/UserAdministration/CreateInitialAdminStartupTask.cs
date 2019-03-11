using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.Framework;
using BettingGame.Framework.Security;
using BettingGame.UserManagement.Core.Domain;
using BettingGame.UserManagement.Core.Features.Registration.Abstraction;
using BettingGame.UserManagement.Core.Features.Shared.Abstraction;

namespace BettingGame.UserManagement.Core.Features.UserAdministration
{
    internal class CreateInitialAdminStartupTask : IStartupTask
    {
        private const string AdminEmail = "admin@betting.com";

        private const string AdminPassword = "9N#7sY@IdbLT2b27CUv2";

        private readonly IPasswordStorage _passwordStorage;

        private readonly IUserCreator _userCreator;

        public CreateInitialAdminStartupTask(IUserCreator userCreator, IPasswordStorage passwordStorage)
        {
            _userCreator = userCreator;
            _passwordStorage = passwordStorage;
        }

        public async Task Run()
        {
            try
            {
                User user = _userCreator.Create();
                user.Email = AdminEmail;
                user.FirstName = "Admin";
                user.LastName = "Istrator";
                user.Nickname = "Admin";
                user.Roles = new List<string> { UserRoles.Administrator };
                user.PasswordHash = _passwordStorage.Create(AdminPassword);
                await _userCreator.SaveAsync(user);
            }
            catch (DuplicatedEmailException)
            {
                // The initialization took already place.
                // Therefore, do nothing.
            }
        }
    }
}
