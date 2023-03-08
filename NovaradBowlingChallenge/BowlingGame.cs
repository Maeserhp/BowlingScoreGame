using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaradBowlingChallenge
{
    class BowlingGame
    {
        List<Player> Players { get; set; } = new List<Player>();

        public void Start()
        {
            Console.WriteLine("Welcome to Bowling!");
            GetPlayers();
            Console.WriteLine("Great! Now that we have all the players, lets start BOWLING!");

            for (int frame = 0; frame < 10; frame++)
            {
                foreach(Player currentPlayer in Players)
                {
                    var pinsUp = 10;
                    Console.WriteLine($"It's time for {currentPlayer.Name} to start their {frame + 1}{Utility.numSuffix(frame + 1)} frame.");
                    for (int delivery = 0; delivery < 2; delivery++)
                    {
                        if (pinsUp > 0)
                        {
                            Console.WriteLine($"{currentPlayer.Name}: How many pins did you knock over on your {delivery + 1}{Utility.numSuffix(delivery + 1)} delivery?");
                            var downPins = GetPins(pinsUp);
                            pinsUp -= downPins;
                            currentPlayer.ScoreBoard[frame].SetDelivery(delivery + 1,  downPins);

                            if(frame == 9 && (pinsUp == 0 || currentPlayer.ScoreBoard[frame].FirstDelivery == 10))
                            {
                                if(pinsUp == 0) pinsUp = 10;

                                // Special delivery
                                if(delivery == 1)
                                {
                                    Console.WriteLine($"Congratulation you get an extra delivery! How many pins did you knock over?");
                                    currentPlayer.ScoreBoard[frame].SpecialDelivery = GetPins(pinsUp);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Nice Strike!");
                            break;
                        }
                        
                    }
                }
                Console.WriteLine("Great job guys! Here is the scoreboard");
                PrintScoreboard();
            }
        }

        public void Finish()
        {
            Console.WriteLine("Nice Game!\nPress Enter to close the game.");
            Console.ReadLine();
        }

        /// <summary>
        /// Ask the user to input how many players they are and what their names are
        /// </summary>
        private void GetPlayers()
        {
            Console.WriteLine("How many players will be playing?");

            int playerCount = Utility.readInt();

            for (int i = 1; i <= playerCount; i++)
            {
                Console.WriteLine($"What is the {i}{Utility.numSuffix(i)} players name?");
                string name = Console.ReadLine();
                Players.Add(new Player(name));
            }
        }

        /// <summary>
        /// Get how many pins were knocked over
        /// </summary>
        /// <param name="pinsUp"> The number of pins that are up at the start of the frame. 
        /// Used to validate that the number of pins knocked over is not more than the pins that are up </param>
        /// <returns></returns>
        private int GetPins(int pinsUp = 10)
        {
            bool isValid = false;
            int knockedPins = -1;
            while(!isValid) 
            {
                knockedPins = Utility.readInt();
                {
                    if (knockedPins < 0)
                    {
                        Console.WriteLine($"Come on, you're not THAT bad! You can't knock over {knockedPins} pins. Enter a number greater than or equal to 0");
                    }
                    else if (knockedPins > pinsUp)
                    {
                        Console.WriteLine($"Sorry, you can't know over {knockedPins} pins when there are only {pinsUp} pins up");
                    }
                    else
                    {
                        Console.WriteLine("Nice Throw!");
                        isValid = true;
                    }
                }
            }
            return knockedPins;
        }

        private void PrintScoreboard()
        {
            /* This is the desired output after each frame

                   1    2    3    4    5    6    7    8    9    10
                  |3|2||4|5||2|/||3|3||X| ||2|3||2|3||2|3||2|3||2|3|5|
          Player1 |  5|| 14|| 27|| 33|| 45|| 50|| 50|| 50|| 50||   50|
                  |3|2||4|5||2|/||3|3||X| ||2|3||2|3||2|3||2|3||2|3|5|
          Player2 |  5|| 14|| 27|| 33|| 45|| 50|| 50|| 50|| 50||   50|

           */

            int longestName = Players.Select(x => x.Name.Length).Max();
            string bumper = new(' ', longestName + 1);
            string output = $"{bumper}  1    2    3    4    5    6    7    8    9    10\n";

            // For eachh player create a string for the frames line and a string for the score line and add them to the output
            foreach(Player player in Players)
            {
                string nameBumper = new(' ', bumper.Length - player.Name.Length);
                string deliveries = $"{bumper}";
                string scores = $"{player.Name}{nameBumper}";

                foreach (Frame frame in player.ScoreBoard)
                {
                    if (!frame.IsLastFrame)
                    {
                        deliveries += $"|{frame.GetFirstMark()}|{frame.GetSecondMark()}|";
                        scores += $"|{ScoreBumper(frame.GetScore())}{frame.GetScore()}|";
                    }
                    //The last frame can have 3 deliveries and needs to be handled uniquely
                    else
                    {
                        deliveries += $"|{frame.GetFirstMark()}|{frame.GetSecondMark()}|{frame.GetSpecialMark()}|";
                        scores += $"|  {ScoreBumper(frame.GetScore())}{frame.GetScore()}|";
                    }
                }
                output += $"{deliveries}\n{scores}\n";
            }
            Console.WriteLine(output);
        }

        /// <summary>
        /// The amount of space that comes befoe the score when printing the score board
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        private string ScoreBumper(int? score)
        {
            // No score
            if(score == 0 || score == null)
            {
                return "   ";
            } 
            // Single digit
            else if(score - 10 < 0)
            {
                return "  ";
            } 
            // Double digit
            else if(score - 100 < 0)
            {
                return " ";
            }
            // Triple digit
            return "";
        }
    }
}
