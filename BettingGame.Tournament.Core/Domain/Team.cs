using System;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.Tournament.Core.Domain
{
    public class Team
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
        [MaxLength(3)]
        public string FifaCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
