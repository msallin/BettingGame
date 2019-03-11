using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Features.TeamAdministration
{
    public class DeleteTeamCommand : ICommand
    {
        [Required]
        public Guid Id { get; set; }
    }
}
