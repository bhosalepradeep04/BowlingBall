namespace BowlingBall.Tests.Frame
{
    using System.Collections.Generic;
    using System.Linq;
    using global::BowlingBall.Interfaces;
    using global::BowlingBall.Models;
    using global::BowlingBall.Models.Enums;
    using global::BowlingBall.Services;
    using Moq;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="UpdateFrames" />.
    /// </summary>
    public class UpdateFrames
    {
        /// <summary>
        /// Defines the frameService.
        /// </summary>
        private readonly IFrameService frameService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateFrames"/> class.
        /// </summary>
        public UpdateFrames()
        {
            var moqScoreService = new Mock<IScoreService>();
            this.frameService = new FrameService(moqScoreService.Object);
        }

        /// <summary>
        /// The WhenUpdateFrames_WithValidPinsForNewFrame_ShouldCreateNewFrame.
        /// </summary>
        /// <param name="gameDetail">The gameDetail<see cref="GameDetailModel"/>.</param>
        /// <param name="pins">The pins<see cref="int"/>.</param>
        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        [Trait("FrameService", nameof(UpdateFrames))]
        public void WhenUpdateFrames_WithValidPinsForNewFrame_ShouldCreateNewFrame(GameDetailModel gameDetail, int pins)
        {
            // Arrange
            var frameCount = gameDetail.Frames.Count;
            var currentFrameNumber = gameDetail.Frames.OrderBy(x => x.FrameNumber).FirstOrDefault()?.FrameNumber ?? 0;

            // Act
            this.frameService.UpdateFrames(gameDetail, pins);

            // Assert
            Assert.Equal(frameCount + 1, gameDetail.Frames.Count);

            var currentUpdatedFrameNumber = gameDetail.Frames.OrderByDescending(x => x.FrameNumber).FirstOrDefault().FrameNumber;
            Assert.Equal(currentFrameNumber + 1, currentUpdatedFrameNumber);
        }

        /// <summary>
        /// The WhenUpdateFrames_WithTenPins_ShouldSetFrameBonusTypeAsStrike.
        /// </summary>
        [Fact]
        [Trait("FrameService", nameof(UpdateFrames))]
        public void WhenUpdateFrames_WithTenPins_ShouldSetFrameBonusTypeAsStrike()
        {
            // Arrange
            var gameDetail = new GameDetailModel();
            int pins = 10;
            EnumBonusType expectedBonusType = EnumBonusType.STRIKE;

            // Act
            this.frameService.UpdateFrames(gameDetail, pins);

            // Assert
            var latestFrame = gameDetail.Frames.OrderByDescending(x => x.FrameNumber).FirstOrDefault();
            Assert.Equal(latestFrame.BonusType, expectedBonusType);
        }

        /// <summary>
        /// The WhenUpdateFrames_WithTwoRollsAddingUpToTenPins_ShouldSetFrameBonusTypeAsSpare.
        /// </summary>
        [Fact]
        [Trait("FrameService", nameof(UpdateFrames))]
        public void WhenUpdateFrames_WithTwoRollsAddingUpToTenPins_ShouldSetFrameBonusTypeAsSpare()
        {
            // Arrange
            var gameDetail = new GameDetailModel();
            int[] arrPins = new int[2] { 7, 3 };
            EnumBonusType expectedBonusType = EnumBonusType.SPARE;

            // Act
            foreach (var pins in arrPins)
            {
                this.frameService.UpdateFrames(gameDetail, pins);
            }

            // Assert
            var latestFrame = gameDetail.Frames.OrderByDescending(x => x.FrameNumber).FirstOrDefault();
            Assert.Equal(latestFrame.BonusType, expectedBonusType);
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <param name="numTests">The numTests<see cref="int"/>.</param>
        /// <returns>The <see cref="IEnumerable{object[]}"/>.</returns>
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var newGameWithZeroFrames = new GameDetailModel() { Frames = new List<FrameModel>() };
            var gameWithOneFrame = new GameDetailModel()
            {
                Frames = new List<FrameModel>(){
                    new FrameModel()
                    {
                        FrameNumber = 1,
                        BonusType = Models.Enums.EnumBonusType.STRIKE,
                        // PendingRollCounter 1: which is (2-1) as 
                        // Current frame has Strike and next frame's 1st roll is done and current request is for Frame-2 Roll-2
                        PendingRollCounter = 1,
                        Rolls = new List<int>(){ 10 },
                        Score = 0,
                    }
                }
            };

            // First roll in the game (Strike)
            // 0 frames: first roll in the game.
            yield return new object[] { newGameWithZeroFrames, 10 };

            // Second roll in the game (1st was Strike)
            yield return new object[] { gameWithOneFrame, 7 };
        }
    }
}
