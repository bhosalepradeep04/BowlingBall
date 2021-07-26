namespace BowlingBall.Interfaces
{
    using BowlingBall.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IGame" />.
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// The Roll.
        /// </summary>
        /// <param name="pins">The pins<see cref="int"/>.</param>
        void Roll(int pins);

        /// <summary>
        /// The GetScore.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        int GetScore();

        /// <summary>
        /// The GetFrames.
        /// </summary>
        /// <returns>The <see cref="List{FrameModel}"/>.</returns>
        List<FrameModel> GetFrames();
    }
}
