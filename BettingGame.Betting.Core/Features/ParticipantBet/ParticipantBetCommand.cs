using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.Betting.Core.Features.ParticipantBet
{
    public class ParticipantBetCommand : ICommand
    {
        [Required]
        public Guid GameId { get; set; }

        [Required]
        [Range(0, 50)]
        public int ScoreTeamA { get; set; }

        [Required]
        [Range(0, 50)]
        public int ScoreTeamB { get; set; }
    }
}
