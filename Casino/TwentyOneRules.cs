using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.TwentyOne
{
    public class TwentyOneRules
    {
        // Dictionary of card values
        public static Dictionary<Face, int> _cardValues = new Dictionary<Face, int>()
        {
            [Face.Two] = 2,
            [Face.Three] = 3,
            [Face.Four] = 4,
            [Face.Five] = 5,
            [Face.Six] = 6,
            [Face.Seven] = 7,
            [Face.Eight] = 8,
            [Face.Nine] = 9,
            [Face.Ten] = 10,
            [Face.Jack] = 10,
            [Face.Queen] = 10,
            [Face.King] = 10,
            [Face.Ace] = 1
        };

        public static int[] GetAllPossibleHandValues(List<Card> Hand)
        {
            int aceCount = Hand.Count(x => x.Face == Face.Ace); // Find out how many Ace's there are
            int[] result = new int[aceCount + 1]; // Plus 1 means if there's 2 Aces, there's 3 possible results ((1,1)||(1,11)||(11,11))
            int value = Hand.Sum(x => _cardValues[x.Face]); // Value is the lowest possible value with all Ace's == 1
            result[0] = value;
            if (result.Length == 1) return result;
            for (int i = 1; i < result.Length; i++)
            {
                value += (i * 10);
                result[i] = value;
            }
            return result;
        }



        public static bool CheckForBlackJack(List<Card> Hand)
        {
            int[] possibleValues = GetAllPossibleHandValues(Hand);
            int value = possibleValues.Max();
            if (value == 2) return true;
            else return false;
        }

        public static bool IsBusted(List<Card> Hand)
        {
            int value = GetAllPossibleHandValues(Hand).Min();
            if (value > 21) return true;
            else return false;
        }

        public static bool ShouldDealerStay(List<Card> Hand)
        {
            int[] possibleHandvalues = GetAllPossibleHandValues(Hand);
            foreach (int value in possibleHandvalues)
            {
                if (value > 16 && value < 22)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool? CompareHands(List<Card> PlayerHand, List<Card> DealerHand)
        {
            int[] playerResults = GetAllPossibleHandValues(PlayerHand);
            int[] dealerResults = GetAllPossibleHandValues(DealerHand);

            int playerScore = playerResults.Where(x => x < 22).Max(); // Filter the values that's < 22 and bring me the max of the values
            int dealerScore = dealerResults.Where(x => x < 22).Max();

            if (playerScore > dealerScore) return true;
            else if (playerScore < dealerScore) return false;
            else return null; // this is a tie
        }

        public static int PlayerCardValue(List<Card> PlayerHand)
        {
            int[] playerResults = GetAllPossibleHandValues(PlayerHand);
            int playerScore = playerResults.Where(x => x < 100).DefaultIfEmpty().Max();
            return playerScore;
        }

        public static int DealerCardValue(List<Card> DealerHand)
        {
            int[] dealerResults = GetAllPossibleHandValues(DealerHand);
            int dealerScore = dealerResults.Where(x => x < 100).DefaultIfEmpty().Max();
            return dealerScore;
        }



        //public static int DealerCardValue(List<Card> DealerHand)
        //{
        //    int[] dealerResults = GetAllPossibleHandValues(DealerHand);
        //    int dealerScoreMin = dealerResults.Where(x => x < 22).DefaultIfEmpty().Min();
        //    int dealerScoreMax = dealerResults.Where(x => x >= 22).DefaultIfEmpty().Max();

        //    if (dealerScoreMax > 22)
        //        return dealerScoreMax;
        //    else
        //        return dealerScoreMin; 
        //}
    }
}
