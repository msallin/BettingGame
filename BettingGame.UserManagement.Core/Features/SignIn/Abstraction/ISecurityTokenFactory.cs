using BettingGame.UserManagement.Core.Domain;

namespace BettingGame.UserManagement.Core.Features.SignIn.Abstraction
{
    public interface ISecurityTokenFactory
    {
        string Create(User user);
    }
}
