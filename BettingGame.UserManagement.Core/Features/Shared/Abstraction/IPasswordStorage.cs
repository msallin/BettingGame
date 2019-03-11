namespace BettingGame.UserManagement.Core.Features.Shared.Abstraction
{
    internal interface IPasswordStorage
    {
        string Create(string password);

        bool Match(string inputPassword, string originalPassword);
    }
}
