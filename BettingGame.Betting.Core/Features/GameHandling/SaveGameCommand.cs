using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.Betting.Core.Features.GameHandling
{
    public class SaveGameMetadata : ICommand
    {
        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset StartDate { get; set; }

        public Guid? TeamA { get; set; }

        public Guid? TeamB { get; set; }
    }
}
