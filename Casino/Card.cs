using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    // Make Card a struct because we don't want the value of the card to change if someone referenced a particular card and changed the face/suit
    public struct Card
    {
        // The Card class has a property of data type string called Suit.  You can get this property or set this property.
        public Suit Suit { get; set; }    
        public Face Face { get; set; }

        public override string ToString()
        {
            return string.Format($"{Face} of {Suit}");
        }
    }

    public enum Suit
    {
        Clubs,      // [0]
        Diamonds,   // [1]
        Hearts,     // [2]
        Spades      // [3]
    }

    public enum Face
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

}
