using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Features.TeamAdministration
{
    public class UpdateTeamCommand : ICommand
    {
        [Required]
        [MaxLength(1)]
        public string Group { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(6)]
        public string Iso2 { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
