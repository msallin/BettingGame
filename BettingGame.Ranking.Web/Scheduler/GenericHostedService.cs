using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Silverback.Messaging.Messages;
using Silverback.Messaging.Publishing;

namespace BettingGame.Ranking.Web.Scheduler
{
    public class GenericHostedService : BackgroundService
    {
        private readonly Func<ICommand> _commandFactory;

        private readonly TimeSpan _delay;

        private readonly IServiceProvider _serviceProvider;

        public GenericHostedService(IServiceProvider serviceProvider, Func<ICommand> commandFactory, TimeSpan delay)
        {
            _serviceProvider = serviceProvider;
            _commandFactory = commandFactory;
            _delay = delay;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (IServiceScope serviceScope = _serviceProvider.CreateScope())
            {
                ICommandPublisher commandPublisher = serviceScope.ServiceProvider.GetRequiredService<ICommandPublisher>();

                while (!cancellationToken.IsCancellationRequested)
                {
                    await commandPublisher.ExecuteAsync(_commandFactory());
                    await Task.Delay(_delay, cancellationToken);
                }
            }
        }
    }
}
