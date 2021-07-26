namespace BowlingBall.Services
{
    using BowlingBall.Interfaces;
    using BowlingBall.Models;
    using BowlingBall.Models.Enums;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="FrameService" />.
    /// </summary>
    public class FrameService : IFrameService
    {
        /// <summary>
        /// Defines the scoreService.
        /// </summary>
        private readonly IScoreService scoreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameService"/> class.
        /// </summary>
        /// <param name="scoreService">The scoreService<see cref="IScoreService"/>.</param>
        public FrameService(IScoreService scoreService)
        {
            this.scoreService = scoreService;
        }

        /// <summary>
        /// The UpdateFrames.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        /// <param name="pins">The pins<see cref="int"/>.</param>
        public void UpdateFrames(GameDetailModel gameDetail, int pins)
        {
            // Get current frame
            var currentFrame = this.GetCurrentFrame(gameDetail);

            // Update current frame
            this.UpdateCurrentFrame(currentFrame, pins);

            // Update Pending Roll Counter for previous frames
            gameDetail.Frames.Where(x => x.FrameNumber != currentFrame.FrameNumber
                                           && x.PendingRollCounter > 0)
                             .Select(x => { x.PendingRollCounter--; return x; }).ToList();

            // Update scores
            this.scoreService.UpdateScores(gameDetail);
        }

        /// <summary>
        /// The GetCurrentFrame.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        /// <returns>The <see cref="FrameModel"/>.</returns>
        private FrameModel GetCurrentFrame(GameDetailModel gameDetail)
        {
            // Add first frame if rolling for first time.
            if (gameDetail.Frames.Count == 0)
            {
                this.CreateFrame(gameDetail);
            }

            // Return latest frame if not reached 2 rolls and first roll with strike else return new frame.
            var latestFrame = gameDetail.Frames.OrderByDescending(x => x.FrameNumber).FirstOrDefault();
            var currentFrame = (latestFrame.Rolls.Count == 2 || latestFrame.BonusType == EnumBonusType.STRIKE) ?
                                this.CreateFrame(gameDetail) : latestFrame;
            return currentFrame;
        }

        /// <summary>
        /// The CreateFrame.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        /// <returns>The <see cref="FrameModel"/>.</returns>
        private FrameModel CreateFrame(GameDetailModel gameDetail)
        {
            var frame = new FrameModel() { FrameNumber = gameDetail.Frames.Count + 1 };
            gameDetail.Frames.Add(frame);
            return frame;
        }

        /// <summary>
        /// The UpdateCurrentFrame.
        /// </summary>
        /// <param name="currentFrame">The currentFrame<see cref="FrameModel"/>.</param>
        /// <param name="pins">The pins<see cref="int"/>.</param>
        private void UpdateCurrentFrame(FrameModel currentFrame, int pins)
        {
            currentFrame.Rolls.Add(pins);

            // BonusType: If 10 pins then Strike or if score is 10 for 2 rolls in a frame then Spare else None.
            currentFrame.BonusType = (pins == 10) ? EnumBonusType.STRIKE :
                                    (currentFrame.Rolls.Sum() == 10) ? EnumBonusType.SPARE : EnumBonusType.NONE;

            // Set PendingRollCounter for Strike as 2, for Spare as 1 and for None as 0
            // But if it is None and frame rolls are not completed, set as -1 to avoid for score calculation
            currentFrame.PendingRollCounter = (currentFrame.BonusType == EnumBonusType.NONE && currentFrame.Rolls.Count < 2) ?
                                                -1 : (int)currentFrame.BonusType;
        }
    }
}
