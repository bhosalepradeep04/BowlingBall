namespace BowlingBall.Tests.BowlingBall
{
    using global::BowlingBall.Interfaces;
    using global::BowlingBall.Services;
    using System.Collections.Generic;
    using Xunit;

    /// <summary>
    /// Defines the <see cref="BowlingBall" />.
    /// </summary>
    public class BowlingBall
    {
        /// <summary>
        /// Defines the game.
        /// </summary>
        private readonly IGame game;

        /// <summary>
        /// Initializes a new instance of the <see cref="BowlingBall"/> class.
        /// </summary>
        public BowlingBall()
        {
            IScoreService scoreService = new ScoreService();
            IFrameService frameService = new FrameService(scoreService);
            this.game = new Game(frameService);
        }

        /// <summary>
        /// The WhenUpdateScores_WithMultipleFrames_ShouldUpdateCurrentScore.
        /// </summary>
        /// <param name="gameRolls">The gameRolls<see cref="List{int}"/>.</param>
        /// <param name="expectedFinalScore">The expectedFinalScore<see cref="int"/>.</param>
        [Theory]
        [MemberData(nameof(GetData), parameters: 2)]
        [Trait("FrameService", nameof(BowlingBall))]
        public void WhenUpdateScores_WithMultipleFrames_ShouldUpdateCurrentScore(List<int> gameRolls, int expectedFinalScore)
        {
            // Act
            foreach (var pins in gameRolls)
            {
                this.game.Roll(pins);
            }

            var actualScore = this.game.GetScore();

            // Assert
            Assert.Equal(expectedFinalScore, actualScore);
        }

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <param name="numTests">The numTests<see cref="int"/>.</param>
        /// <returns>The <see cref="IEnumerable{object[]}"/>.</returns>
        public static IEnumerable<object[]> GetData(int numTests)
        {
            var game1Rolls = new List<int>() { 10, 9, 1, 5, 5, 7, 2, 10, 10, 10, 9, 0, 8, 2, 9, 1, 10 };
            var game2Rolls = new List<int>() { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 2, 10, 8, 2, 9 };

            // Expected: '187'
            yield return new object[] { game1Rolls, 187 };

            // Expected: '184'
            yield return new object[] { game2Rolls, 184 };
        }
    }
}
