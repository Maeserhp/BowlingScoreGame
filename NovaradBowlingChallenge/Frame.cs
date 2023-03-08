using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaradBowlingChallenge
{
    class Frame
    {
        public int? FirstDelivery { get; set; } = null;
        public int? SecondDelivery { get; set; } = null;
        public int? SpecialDelivery { get; set; } = null;
        private int Score { get; set; }
        public Frame NextFrame { get; set; }
        public Frame PreviousFrame { get; set; }
        public bool IsLastFrame { get; }

        public Frame(bool isLastFrame = false)
        {
            this.IsLastFrame = isLastFrame;
        }

        private bool isStrike()
        {
            if (FirstDelivery == 10) return true;
            return false;
        }
        private bool isSpare()
        {
            if (!isStrike() && FirstDelivery + SecondDelivery == 10) return true;
            return false;
        }

        public int? GetDelivery(int num)
        {
            switch (num)
            {
                case 1:
                    return FirstDelivery;
                case 2:
                    return SecondDelivery;
                case 3:
                    return SpecialDelivery;
                default:
                    return null;
            }
        }

        public void SetDelivery(int num, int pins)
        {
            switch (num)
            {
                case 1:
                    FirstDelivery = pins;
                    break;
                case 2:
                    SecondDelivery = pins;
                    break;
                case 3:
                    SpecialDelivery = pins;
                    break;
            }
        }

        public string GetFirstMark()
        {
            return GetMark(FirstDelivery);
        }

        public string GetSecondMark()
        {
            if (IsLastFrame && SecondDelivery == 10) return "X";
            else if (isStrike() && !IsLastFrame) return " ";
            else if (isSpare()) return "/";
            //Not yet bowled
            else if (SecondDelivery == null) return " ";
            else return SecondDelivery.ToString();
        }

        public string GetSpecialMark()
        {
            return GetMark(SpecialDelivery);
        }

        private string GetMark(int? delivery)
        { 
            switch (delivery)
            {
                case 10: //Strike
                    return "X";
                case null: //Not yet bowled
                    return " ";
                default:
                    return delivery.ToString();

            }
        }

        public int? GetScore()
        {
            //Has the score already been recorded for this frame?
            if (Score != 0) return Score;

            //Has this frame even been bowled yet?
            if (!FirstDelivery.HasValue) return null;

            Score = FirstDelivery.GetValueOrDefault() + SecondDelivery.GetValueOrDefault();

            if (IsLastFrame)
            {
                Score += SpecialDelivery.GetValueOrDefault();
            }
            else if (isStrike())
            {
                //If the next frame has not been bowled yet then we cannot return anything
                if (!NextFrame.FirstDelivery.HasValue) { Score = 0; return null; }

                //We need to get the values of the next two deliveries
                // 1
                Score += NextFrame.FirstDelivery.Value;

                // 2
                if (NextFrame.SecondDelivery.HasValue)
                {
                    Score += NextFrame.SecondDelivery.Value;
                }
                else if (NextFrame.NextFrame.FirstDelivery.HasValue)
                {
                    Score += NextFrame.NextFrame.FirstDelivery.Value;
                }
                else { Score = 0; return null; }
            }
            else if (isSpare())
            {
                if (NextFrame.FirstDelivery.HasValue)
                {
                    Score += NextFrame.FirstDelivery.Value;
                }
                else { Score = 0; return null; }
            }
           
            //Add the previous frames score to the current frames score
            if (PreviousFrame != null)
            {
                Score += PreviousFrame.GetScore().Value;
            }

            return Score;
        }
    }
}
