using System;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.Ranking.Core.Domain
{
    public class RankingEntry
    {
        [Required]
        public int Score { get; set; }

        [Required]
        public string UserFirstName { get; set; } = "Unknown";

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string UserLastName { get; set; } = "Unknown";

        [Required]
        public string UserNickname { get; set; } = "Unknown";

        [Required]
        public string UserGravatarHash { get; set; } = "Unknown";

        [Required]
        public int Rank { get; set; }
    }
}
