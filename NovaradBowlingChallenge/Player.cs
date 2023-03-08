using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaradBowlingChallenge
{
    class Player
    {
        public string Name { get; }
        public Frame[] ScoreBoard { get; set; } = new Frame[10];

        public Player(string name)
        {
            this.Name = name.ToUpper();

            CreateScoreBoard();
        }

        private void CreateScoreBoard()
        {
            //Create a new frame object for each frame in the scoreboard
            for (int i = 0; i < ScoreBoard.Count(); i++)
            {
                bool isLastFrame = i == ScoreBoard.Count() - 1 ? true : false;
                Frame currentFrame = new Frame(isLastFrame);

                // Connect the next and previous Frames
                int previousIdx = i - 1;
                if (previousIdx >= 0)
                {
                    currentFrame.PreviousFrame = ScoreBoard[previousIdx];
                    ScoreBoard[previousIdx].NextFrame = currentFrame;

                }
                ScoreBoard[i] = currentFrame;
            }
        }
    }
}
