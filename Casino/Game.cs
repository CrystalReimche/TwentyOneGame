using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    // abstract - this class (Game) can never be instantiated, it can never be an object, 
    // it's only meant to be inherited from. 
    // We are never going to make an instance of Game, it's always going to be a specific
    // type of game like Poker, Solitaire, Texas Holdem
    public abstract class Game  
    {
        /* *************************************************************************
         *                            CLASS PROPERTY
         * ********************************************************************** */
        private List<Player> _players = new List<Player>(); // This will set an empty list
        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();

        public List<Player> Players { get { return _players; } set { _players = value; } }
        public Dictionary<Player, int> Bets { get { return _bets; } set { _bets = value; } }

        public string Name { get; set; }
        

        /* *************************************************************************
         *                        ABSTRACT PLAY METHOD
         * ********************************************************************** */
        // Abstract method can only exist inside an abstract class.
        // Abstract method contains no implementation
        // All it does is state "Any class that is inheritating this one, 
        // MUST implement this method."
        public abstract void Play();

        /* *************************************************************************
         *                        VIRTUAL LISTPLAYERS METHOD
         * ********************************************************************** */
        // Virtual method can only exist inside an abstract class.
        // Virtual method contains implementation
        // This ListPlayers() is accessible to inheriting classes like TwentyOneGame.
        // TwentyOneGame has the ability to override this method though.
        public virtual void ListPlayers()
        {
            foreach (Player Player in Players)
            {
                Console.WriteLine(Player);

            }
        }
    }
}
