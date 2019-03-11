using System;
using System.Threading;
using System.Threading.Tasks;

using Silverback.Messaging.Messages;
using Silverback.Messaging.Publishing;

namespace BettingGame.Betting.Web.Scheduler
{
    public class GenericHostedService : HostedService
    {
        private readonly ICommandPublisher _commandDispatcher;

        private readonly Func<ICommand> _commandFactory;

        private readonly TimeSpan _delay;

        public GenericHostedService(ICommandPublisher commandDispatcher, Func<ICommand> commandFactory, TimeSpan delay)
        {
            _commandDispatcher = commandDispatcher;
            _commandFactory = commandFactory;
            _delay = delay;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await _commandDispatcher.ExecuteAsync(_commandFactory());
                await Task.Delay(_delay, cancellationToken);
            }
        }
    }
}
