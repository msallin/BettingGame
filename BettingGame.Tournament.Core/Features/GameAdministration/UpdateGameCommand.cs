using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;
using BettingGame.Tournament.Core.Domain;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    public class UpdateGameCommand : ICommand
    {
        [MaxLength(1)]
        public string Group { get; set; }

        [Required]
        public Guid Id { get; set; }

        public DateTimeOffset? StartDate { get; set; }

        public Guid? TeamA { get; set; }

        public Guid? TeamB { get; set; }

        [Required]
        public GameType Type { get; set; }
    }
}
