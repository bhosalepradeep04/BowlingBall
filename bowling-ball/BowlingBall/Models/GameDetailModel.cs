namespace BowlingBall.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="GameDetailModel" />.
    /// </summary>
    public class GameDetailModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameDetailModel"/> class.
        /// </summary>
        public GameDetailModel()
        {
            this.Frames = new List<FrameModel>();
        }

        /// <summary>
        /// Gets or sets the Frames.
        /// </summary>
        public List<FrameModel> Frames { get; set; }

        /// <summary>
        /// Gets or sets the Score.
        /// </summary>
        public int Score { get; set; }
    }
}
