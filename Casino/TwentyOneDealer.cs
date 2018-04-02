using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.TwentyOne
{
    // Since dealers have different options with different games, create a dealer for 21
    // that will inherit generic properties from Dealer
    public class TwentyOneDealer : Dealer   
    {
        private List<Card> _hand = new List<Card>();
        public List<Card> Hand { get { return _hand; } set { _hand = value; } }
        public bool Stay { get; set; }
        public bool isBusted { get; set; }


    }
}
