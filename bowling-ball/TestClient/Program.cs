using BowlingBall;
using BowlingBall.Interfaces;
using BowlingBall.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize
            IScoreService scoreService = new ScoreService();
            IFrameService frameService = new FrameService(scoreService);
            IGame game = new Game(frameService);

            // Roll
            game.Roll(10);
            game.Roll(9);
            game.Roll(1);
            game.Roll(5);
            game.Roll(5);
            game.Roll(7);
            game.Roll(2);
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.Roll(9);
            game.Roll(0);
            game.Roll(8);
            game.Roll(2);
            game.Roll(9);
            game.Roll(1);
            game.Roll(10);

            // Score
            foreach (var frame in game.GetFrames().Take(10))
            {
                Console.WriteLine($"Frame {frame.FrameNumber}: {frame.Score}");
            }

            Console.WriteLine($"Final game score: {game.GetScore()}");
            Console.Read();
        }
    }
}
