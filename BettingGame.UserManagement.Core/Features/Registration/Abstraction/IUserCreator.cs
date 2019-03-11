using System.Threading.Tasks;

using BettingGame.UserManagement.Core.Domain;

namespace BettingGame.UserManagement.Core.Features.Registration.Abstraction
{
    public interface IUserCreator
    {
        User Create();

        Task SaveAsync(User user);
    }
}
