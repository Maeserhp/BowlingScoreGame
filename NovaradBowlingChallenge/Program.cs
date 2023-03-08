using System;

namespace NovaradBowlingChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            BowlingGame newGame = new BowlingGame();

            newGame.Start();
            newGame.Finish();
        }
    }
}
