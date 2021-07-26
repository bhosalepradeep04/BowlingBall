namespace BowlingBall.Services
{
    using System.Linq;
    using BowlingBall.Interfaces;
    using BowlingBall.Models;
    using BowlingBall.Models.Enums;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ScoreService" />.
    /// </summary>
    public class ScoreService : IScoreService
    {
        /// <summary>
        /// The UpdateScores.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        public void UpdateScores(GameDetailModel gameDetail)
        {
            // Update scores for all pending frames (May include previous Strike pr Spare frames)
            var unscoredFrames = gameDetail.Frames.Where(x => x.PendingRollCounter == 0).OrderBy(x => x.FrameNumber).ToList();
            if (unscoredFrames?.Count > 0)
            {
                // Get unscored frames: current settled frame (i.e not Strike or Spare) and previous Striked or Spared frames
                foreach (var frame in unscoredFrames)
                {
                    switch (frame.BonusType)
                    {
                        case EnumBonusType.STRIKE:
                            var frames = gameDetail.Frames.Where(x => x.FrameNumber > frame.FrameNumber).ToList();
                            frame.Score = gameDetail.Score + this.GetStrikeScore(frames);
                            break;
                        case EnumBonusType.SPARE:
                            var nextFrameRolls = gameDetail.Frames.Where(x => x.FrameNumber == frame.FrameNumber + 1).FirstOrDefault()?.Rolls;
                            frame.Score = gameDetail.Score + frame.Rolls.Sum() + nextFrameRolls[0];
                            break;
                        default:
                            frame.Score = gameDetail.Score + frame.Rolls.Sum();
                            break;
                    }

                    frame.PendingRollCounter = -1; // Set to -1 to avoid for future score calculations
                    gameDetail.Score = frame.Score;
                }
            }
        }

        /// <summary>
        /// The GetStrikeScore.
        /// </summary>
        /// <param name="frames">The frames<see cref="List{FrameModel}"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int GetStrikeScore(List<FrameModel> frames)
        {
            int score = 10;
            int counter = (int)EnumBonusType.STRIKE;
            frames = frames.OrderBy(x => x.FrameNumber).ToList();
            foreach (var frame in frames)
            {
                if (frame.BonusType != EnumBonusType.STRIKE)
                {
                    // If next frame is not Strike then add 2 rolls and break;
                    score = score + frame.Rolls.Take(counter).Sum();
                    break;
                }
                else
                {
                    // If it is a strike, add Rolls (which is 1 with value as 10)
                    score = score + frame.Rolls.Sum();
                    counter--;
                }

                if (counter == 0)
                {
                    // Breask after adding 2 rolls score
                    break;
                }
            }

            return score;
        }
    }
}
