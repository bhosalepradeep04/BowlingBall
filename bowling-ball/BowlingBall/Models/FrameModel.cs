namespace BowlingBall.Models
{
    using BowlingBall.Models.Enums;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FrameModel" />.
    /// </summary>
    public class FrameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameModel"/> class.
        /// </summary>
        public FrameModel()
        {
            this.Rolls = new List<int>();
        }

        /// <summary>
        /// Gets or sets the FrameNumber.
        /// </summary>
        public int FrameNumber { get; set; }

        /// <summary>
        /// Gets or sets the Rolls.
        /// </summary>
        public List<int> Rolls { get; set; }

        /// <summary>
        /// Gets or sets the BonusType.
        /// </summary>
        public EnumBonusType BonusType { get; set; }

        /// <summary>
        /// Gets or sets the Score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the PendingRollCounter used to keep track of no. of next rolls to be consider for frame score calculation.
        /// </summary>
        public int PendingRollCounter { get; set; }
    }
}
