using Silverback.Messaging.Publishing;

namespace BettingGame.Framework.Web.Controller

{
    public abstract class CqrsControllerBase : Microsoft.AspNetCore.Mvc.Controller
    {
        protected CqrsControllerBase(IQueryPublisher queryPublisher, ICommandPublisher commandPublisher)
        {
            QueryPublisher = queryPublisher;
            CommandPublisher = commandPublisher;
        }

        protected ICommandPublisher CommandPublisher { get; }

        protected IQueryPublisher QueryPublisher { get; }
    }
}
