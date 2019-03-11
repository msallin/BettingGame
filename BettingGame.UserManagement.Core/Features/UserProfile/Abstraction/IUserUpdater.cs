using System;
using System.Threading.Tasks;

using BettingGame.UserManagement.Core.Domain;

namespace BettingGame.UserManagement.Core.Features.UserProfile.Abstraction
{
    public interface IUserUpdater
    {
        Task<User> GetAsync(Guid id);

        Task UpdateAsync(User user);
    }
}
