using System;
using System.ComponentModel.DataAnnotations;

namespace BettingGame.Betting.Core.Domain
{
    public class Bet
    {
        private enum GameResult
        {
            Won,

            Loss,

            Draw
        }

        public Result ActualResult { get; set; }

        public bool ActualResultPresent => ActualResult != null;

        [Required]
        public Result BetResult { get; set; }

        [Required]
        public Guid GameId { get; set; }

        public Guid Id { get; set; }

        [Required]
        public DateTimeOffset LastChanged { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public int GetScoreForBet()
        {
            var score = 0;

            if (!ActualResultPresent || BetResult == null)
            {
                return score;
            }

            if (BetResult.ScoreTeamA == ActualResult.ScoreTeamA)
            {
                score += 2;
            }

            if (BetResult.ScoreTeamB == ActualResult.ScoreTeamB)
            {
                score += 2;
            }

            if (BetResult.ScoreTeamA == ActualResult.ScoreTeamA && BetResult.ScoreTeamB == ActualResult.ScoreTeamB)
            {
                score += 1;
            }

            GameResult betGameResult = BetResult.ScoreTeamA != BetResult.ScoreTeamB ? (BetResult.ScoreTeamA <= BetResult.ScoreTeamB ? GameResult.Loss : GameResult.Won) : GameResult.Draw;
            GameResult actualGameResult = ActualResult.ScoreTeamA != ActualResult.ScoreTeamB ? (ActualResult.ScoreTeamA <= ActualResult.ScoreTeamB ? GameResult.Loss : GameResult.Won) : GameResult.Draw;
            if (betGameResult == actualGameResult)
            {
                score += 5;
            }

            return score;
        }
    }
}
