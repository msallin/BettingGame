using System.ComponentModel.DataAnnotations;

namespace BettingGame.Tournament.Core.Domain
{
    public class Result
    {
        [Range(0, 50)]
        public int ScoreTeamA { get; set; }

        [Range(0, 50)]
        public int ScoreTeamB { get; set; }
    }
}
