using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Casino
{
    public class Dealer
    {
        /* *************************************************************************
         *                            CLASS PROPERTY
         * ********************************************************************** */
        public string Name { get; set; }
        public Deck Deck { get; set; }
        public int Balance { get; set; }


        /* *************************************************************************
         *                            DEAL METHOD
         * ********************************************************************** */
        public void Deal(List<Card> Hand)   
        {
            // Add the first card in the deck and add it to the Hand that is about to Deal()
            Hand.Add(Deck.Cards.First());
            // Log which card that was dealt 
            string card = string.Format(Deck.Cards.First().ToString());
            Console.WriteLine(card);
            using (StreamWriter file = new StreamWriter(@"..\log.txt", true))
            {
                file.WriteLine(DateTime.Now);
                file.WriteLine(card);
            }
            // Remove it from the Deck
            Deck.Cards.RemoveAt(0); // index[0] will always be the first card in the deck, so once that's removed and Deal() calls for the
            
        }
    }
}
