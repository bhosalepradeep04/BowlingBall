namespace BowlingBall
{
    using BowlingBall.Interfaces;
    using BowlingBall.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Game" />.
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        /// Defines the gameDetail.
        /// </summary>
        private GameDetailModel gameDetail = new GameDetailModel();

        /// <summary>
        /// Defines the frameService.
        /// </summary>
        private readonly IFrameService frameService;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="frameService">The frameService<see cref="IFrameService"/>.</param>
        public Game(IFrameService frameService)
        {
            this.frameService = frameService;
        }

        /// <summary>
        /// The Roll.
        /// </summary>
        /// <param name="pins">The pins<see cref="int"/>.</param>
        public void Roll(int pins)
        {
            this.frameService.UpdateFrames(this.gameDetail, pins);
        }

        /// <summary>
        /// The GetScore.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetScore()
        {
            return this.gameDetail.Score;
        }

        /// <summary>
        /// The GetFrames.
        /// </summary>
        /// <returns>The <see cref="List{FrameModel}"/>.</returns>
        public List<FrameModel> GetFrames()
        {
            return this.gameDetail.Frames;
        }
    }
}
