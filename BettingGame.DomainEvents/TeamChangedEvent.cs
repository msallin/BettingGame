using System;
using System.ComponentModel.DataAnnotations;

using Silverback.Messaging.Messages;

namespace BettingGame.DomainEvents
{
    public class TeamChangedEvent : IIntegrationEvent
    {
        [Required]
        [MaxLength(3)]
        public string FifaCode { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
