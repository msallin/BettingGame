using System;
using System.ComponentModel.DataAnnotations;

using BettingGame.Ranking.Core.Domain;

using Silverback.Messaging.Messages;

namespace BettingGame.Ranking.Core.Features.UserScore
{
    public class UserScoreQuery : IQuery<RankingEntry>
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
