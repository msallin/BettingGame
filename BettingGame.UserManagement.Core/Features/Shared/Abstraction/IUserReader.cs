using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using BettingGame.UserManagement.Core.Domain;

namespace BettingGame.UserManagement.Core.Features.Shared.Abstraction
{
    public interface IUserReader
    {
        Task<IEnumerable<User>> AllAsync();

        Task<User> ByEmail(string email);

        Task<User> ByIdAsync(Guid id);
    }
}
