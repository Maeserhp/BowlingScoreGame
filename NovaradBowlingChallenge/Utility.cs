using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaradBowlingChallenge
{
    class Utility
    {
        /// <summary>
        /// Used to read input from the console and only accepts an integer.
        /// </summary>
        /// <returns></returns>
        public static int readInt()
        {
            int value = -1;
            while (!int.TryParse(Console.ReadLine(), out value)){
                Console.WriteLine("that is not a number. Please input a number");
            }

            return value;
        }

        /// <summary>
        /// Finds the suffix of the given integer. i.e. 1st, 2nd, 3rd, 4th, etc.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string numSuffix(int num)
        {
            switch (num)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }

    }
}
