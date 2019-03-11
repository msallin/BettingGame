using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.Tournament.Core.Features.GameAdministration
{
    public class DeleteGameCommand : ICommand
    {
        [Required]
        public Guid Id { get; set; }
    }
}
