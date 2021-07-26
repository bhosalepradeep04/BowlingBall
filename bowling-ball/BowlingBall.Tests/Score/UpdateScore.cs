namespace BowlingBall.Tests.Score
{
    using System.Collections.Generic;
    using global::BowlingBall.Interfaces;
    using global::BowlingBall.Models;
    using global::BowlingBall.Models.Enums;
    using global::BowlingBall.Services;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="UpdateScore" />.
    /// </summary>
    public class UpdateScore
    {
        /// <summary>
        /// Defines the scoreService.
        /// </summary>
        private readonly IScoreService scoreService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateScore"/> class.
        /// </summary>
        public UpdateScore()
        {
            this.scoreService = new ScoreService();
        }

        /// <summary>
        /// The WhenUpdateScores_WithMultipleFrames_ShouldUpdateCurrentScore.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        /// <param name="expectedScore">The expectedScore<see cref="int"/>.</param>
        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        [Trait("FrameService", nameof(UpdateScore))]
        public void WhenUpdateScores_WithMultipleFrames_ShouldUpdateCurrentScore(GameDetailModel gameDetail, int expectedScore)
        {
            // Act
            this.scoreService.UpdateScores(gameDetail);

            // Assert
            Assert.Equal(expectedScore, gameDetail.Score);
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <param name="numTests">The numTests<see cref="int"/>.</param>
        /// <returns>The <see cref="IEnumerable{object[]}"/>.</returns>
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var gameWithTwoFrames = new GameDetailModel()
            {
                Frames = new List<FrameModel>()
                {
                    new FrameModel(){ Rolls = new List<int>(){ 10 }, BonusType = EnumBonusType.STRIKE, FrameNumber = 1, PendingRollCounter = 0 },
                    new FrameModel(){ Rolls = new List<int>(){ 5, 4 }, BonusType = EnumBonusType.NONE, FrameNumber = 2, PendingRollCounter = 0 }
                }
            };
            var gameWithThreeFrames = new GameDetailModel()
            {
                Frames = new List<FrameModel>()
                {
                    new FrameModel(){ Rolls = new List<int>(){ 10 }, BonusType = EnumBonusType.STRIKE, FrameNumber = 1, PendingRollCounter = 0 },
                    new FrameModel(){ Rolls = new List<int>(){ 10 }, BonusType = EnumBonusType.STRIKE, FrameNumber = 2, PendingRollCounter = 0 },
                    new FrameModel(){ Rolls = new List<int>(){ 6, 2 }, BonusType = EnumBonusType.NONE, FrameNumber = 3, PendingRollCounter = 0 }
                }
            };

            // Expected: '28'
            yield return new object[] { gameWithTwoFrames, 28 };

            // Expected: '52'
            yield return new object[] { gameWithThreeFrames, 52 };
        }
    }
}
