using System.Threading.Tasks;

namespace BettingGame.Framework
{
    public interface IStartupTask
    {
        Task Run();
    }
}
