using System;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.Betting.Core.Domain
{
    public class GameMetadata
    {
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset StartDate { get; set; }

        public Guid? TeamA { get; set; }

        public Guid? TeamB { get; set; }
    }
}
