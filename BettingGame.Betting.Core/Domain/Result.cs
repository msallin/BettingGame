using System.ComponentModel.DataAnnotations;

namespace BettingGame.Betting.Core.Domain
{
    public class Result
    {
        [Required]
        [Range(0, 50)]
        public int ScoreTeamA { get; set; }

        [Required]
        [Range(0, 50)]
        public int ScoreTeamB { get; set; }
    }
}
