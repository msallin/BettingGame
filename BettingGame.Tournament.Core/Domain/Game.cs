using System;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.Tournament.Core.Domain
{
    public class Game
    {
        public bool Finished => Result != null;

        [MaxLength(1)]
        public string Group { get; set; }

        public Guid Id { get; set; }

        public Result Result { get; set; }

        [Required]
        public DateTimeOffset StartDate { get; set; }

        public Guid? TeamA { get; set; }

        public Guid? TeamB { get; set; }

        [Required]
        public GameType Type { get; set; }
    }
}
