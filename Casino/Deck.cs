using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public class Deck
    {
        /* *************************************************************************
         *                              CONSTRUCTOR
         * ********************************************************************** */
        // Constructor - Constructor name is always the name of the class!!
        public Deck()
        {
            Cards = new List<Card>();   // Create an empty List for the cards to be added to

            for (int i = 0; i < 13; i++)    // Looping through all 13 possibilities of Faces
            {
                for (int j = 0; j < 4; j++) // Looping through all 4 possibilities of Suits
                {
                    Card card = new Card(); // This loop will loop through 52 times (13*4) and each loop will create a new card
                    card.Face = (Face)i;    // Casting a Face to whatever index i is on.  If i == 2, Face will be Hearts
                    card.Suit = (Suit)j;    // Casting a Suit to whatever index j is on.  If j == 9, Face will be Ten
                    Cards.Add(card);    // Adding new card to the deck
                }
            }
        }


        /* *************************************************************************
         *                            CLASS PROPERTY
         * ********************************************************************** */
        // A list of cards using the Cards class 
        public List<Card> Cards { get; set; }


        /* *************************************************************************
         *                            SHUFFLE METHOD
         * ********************************************************************** */
        // When a new deck of cards is instantiated, and the shuffle method is called on that new
        // deck of cards, I want it to shuffle that new deck that was just created
        public void Shuffle(int times = 1)    // times is an optional parameter
        {
            for (int i = 0; i < times; i++)
            {
                // Grab a random card, take it out of the deck, put it in the temporary deck until no more cards are in the deck
                List<Card> TempList = new List<Card>();
                Random random = new Random();

                while (this.Cards.Count > 0)
                {
                    int randomIndex = random.Next(0, this.Cards.Count); // Create integer between 0 and end of card deck
                    TempList.Add(this.Cards[randomIndex]);  // Add that random card to the temporary deck
                    this.Cards.RemoveAt(randomIndex);   // Remove that random card from the original deck
                }
                this.Cards = TempList;  // Now that the original deck is empty, pass in all the cards in the random order from temporary deck
            }
        }
    }
}
