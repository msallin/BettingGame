using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Domain
{
    public class GameResultEvent : IEvent
    {
        public Guid GameId { get; set; }

        [Range(0, 50)]
        public int ScoreTeamA { get; set; }

        [Range(0, 50)]
        public int ScoreTeamB { get; set; }
    }
}
