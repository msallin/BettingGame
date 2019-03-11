using System.Collections.Generic;

using Silverback.Messaging.Messages;

namespace BettingGame.Betting.Core.Features.ExportBetResults
{
    public class BetsWithResultQuery : IQuery<IEnumerable<BetsWithResultQueryResult>>
    { }
}
