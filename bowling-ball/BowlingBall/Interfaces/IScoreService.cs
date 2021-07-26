namespace BowlingBall.Interfaces
{
    using BowlingBall.Models;

    /// <summary>
    /// Defines the <see cref="IScoreService" />.
    /// </summary>
    public interface IScoreService
    {
        /// <summary>
        /// The UpdateScores.
        /// </summary>
        /// <param name="gameDetailModel">The gameDetail<see cref="GameDetailModel"/>.</param>
        void UpdateScores(GameDetailModel gameDetail);
    }
}
