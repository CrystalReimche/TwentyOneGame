using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Player
    {
        /* *************************************************************************
         *                            CLASS PROPERTY
         * ********************************************************************** */
        private List<Card> _hand = new List<Card>();
        public List<Card> Hand { get { return _hand; } set { _hand = value; } }

        public int Balance { get; set; }
        public string Name { get; set; }
        public bool isActivelyPlaying { get; set; }
        public bool Stay { get; set; }

        /* *************************************************************************
         *                            CONSTRUCTOR
         * ********************************************************************** */
        public Player(string name, int beginningBalance)
        {
            Hand = new List<Card>();    // Create an empty hand whenever a new player is created
            Balance = beginningBalance;
            Name = name;
        }

        /* *************************************************************************
         *                            BET OVERLOAD
         * ********************************************************************** */
        public bool Bet (int amount)
        {
            if (Balance - amount < 0)
            {
                Console.WriteLine("You do not have enough to place a bet that size");
                return false;
            }
            else
            {
                Balance -= amount;
                return true;
            }
        }



        /* *************************************************************************
         *                            ADD OPERATOR OVERLOAD
         * ********************************************************************** */
        public static Game operator+ (Game game, Player player)    // We are adding a game and player and returning a game
        {
            game.Players.Add(player);   // It takes a player, adds it to game, and then returns the game
            return game;
        }

        /* *************************************************************************
         *                         SUBTRACT OPERATOR OVERLOAD
         * ********************************************************************** */
         public static Game operator- (Game game, Player player)
        {
            game.Players.Remove(player);
            return game;
        }








    }
}
