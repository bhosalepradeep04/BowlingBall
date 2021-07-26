namespace BowlingBall.Interfaces
{
    using BowlingBall.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IFrameService" />.
    /// </summary>
    public interface IFrameService
    {
        /// <summary>
        /// The UpdateFrames.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        /// <param name="pins">The pins<see cref="int"/>.</param>
        void UpdateFrames(GameDetailModel gameDetail, int pins);
    }
}
