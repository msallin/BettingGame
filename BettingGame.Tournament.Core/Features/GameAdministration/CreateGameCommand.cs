using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;
using BettingGame.Tournament.Core.Domain;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    public class CreateGameCommand : ICommand
    {
        [MaxLength(1)]
        public string Group { get; set; }

        [Required]
        public DateTimeOffset Start { get; set; }

        public Guid? TeamA { get; set; }

        public Guid? TeamB { get; set; }

        [Required]
        public GameType Type { get; set; }
    }
}
