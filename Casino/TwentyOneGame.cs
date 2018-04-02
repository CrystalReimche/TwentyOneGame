using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino.TwentyOne
{
    public class TwentyOneGame  : Game, IWalkAway
    {
        // Property
        public TwentyOneDealer Dealer { get; set; }

        // Since Play() is an abstract method from Game,
        // you must use the override keyword to implement
        public override void Play()
        {
            // With every new game create a dealer
            Dealer = new TwentyOneDealer();
            foreach (Player player in Players)  // Players is a prop of Game which holds a list of each Player
            {
                player.Hand = new List<Card>(); // Reset players hands to empty
                player.Stay = false; // Reset stay == false because it could be true at the end of the game
            }
            Dealer.Hand = new List<Card>(); // Reset dealer hand to empty
            Dealer.Stay = false;
            Dealer.Deck = new Deck(); // Use a brand new deck of cards
            Dealer.Deck.Shuffle();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\nPlace your bet: ");
            Console.ResetColor();

            // Loop through each player and have them place a bet
            foreach (Player player in Players) // Players is a prop of Game which holds a list of each Player
            {
                int bet = Convert.ToInt32(Console.ReadLine());
                bool successfullyBet = player.Bet(bet); // Passing in bet amount into the Bet method on Player
                if (!successfullyBet)
                {
                    return; // This will end the Play() and start the While loop in Main() and see if player is still active and have enough money
                }
                Bets[player] = bet; // Dictionary.. setting the key to player and the value to the amount they bet
                
            }

            for (int i = 0; i < 2; i++) // Only dealing 2 cards to each player
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nDEALING...");
                Console.ResetColor();
                foreach (Player player in Players)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{player.Name}: "); // Stating who got the card
                    Console.ResetColor();
                    Dealer.Deal(player.Hand); // Passing in players hand and giving it a card
                    if (i == 1) // This is the second card that's dealt
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand); // Passing in players hand to check for blackjack
                        if (blackJack)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"Blackjack! {player.Name} wins {Bets[player]} ");
                            Console.ResetColor();
                            player.Balance += Convert.ToInt32((Bets[player] * 1.5) + Bets[player]); // If you win 21, you get your bet * 1.5 plus your original bet amount
                            return;
                        }
                    }
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Dealer: ");
                Console.ResetColor();
                Dealer.Deal(Dealer.Hand);
                if (i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Dealer has BlackJack!  Everyone loses!");
                        Console.ResetColor();
                        foreach (KeyValuePair<Player, int> entry in Bets) // If dealer has 21, go through each player and get all their bets
                        {
                            Dealer.Balance += entry.Value; // put bets into dealer balance
                        }
                        return;
                    }
                }
            }
            // Ask each player if they want to hit or stay
            foreach (Player player in Players)
            {
                while (!player.Stay)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Your cards are:");
                    Console.ResetColor();
                    foreach (Card card in player.Hand)
                    {
                        Console.WriteLine(card.ToString()); // This is overriden in Card class
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\nHit or Stay? ");
                    Console.ResetColor();
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "stay" || answer == "s")
                    {
                        player.Stay = true;
                        break; // If they say stay, everything stops inside the while loop
                    }
                    else if (answer == "hit" || answer == "h")
                    {
                        Dealer.Deal(player.Hand);
                    }
                    bool busted = TwentyOneRules.IsBusted(player.Hand);
                    if (busted)
                    {
                        Console.WriteLine();
                        Dealer.Balance += Bets[player];
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"{player.Name} Busted!");
                        Console.ResetColor();
                        Console.WriteLine($"Dealer had {TwentyOneRules.DealerCardValue(Dealer.Hand)} and {player.Name} had {TwentyOneRules.PlayerCardValue(player.Hand)}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"You lose your bet of {Bets[player]}.  Your balance is now {player.Balance}");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("\n\nDo you want to play again? ");
                        Console.ResetColor();
                        answer = Console.ReadLine().ToLower();
                        if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya" || answer == "yea")
                        {
                            player.isActivelyPlaying = true;
                            return;
                        }
                        else
                        {
                            player.isActivelyPlaying = false;
                            return;
                        }
                    }
                }
            }

            Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            while (!Dealer.Stay && !Dealer.isBusted)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nDealer is hitting...");
                Console.ResetColor();
                Dealer.Deal(Dealer.Hand);
                Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            }
            if (Dealer.Stay)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nDealer is staying.\n");
                Console.ResetColor();
            }
            if (Dealer.isBusted)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\nDealer Busted!");
                Console.ResetColor();
                foreach (Player player in Players)
                {
                    player.Balance += Convert.ToInt32(Bets[player] * 2);
                    Dealer.Balance -= Bets[player];
                    Console.WriteLine($"Dealer had {TwentyOneRules.DealerCardValue(Dealer.Hand)} and {player.Name} had {TwentyOneRules.PlayerCardValue(player.Hand)}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{player.Name} won {Bets[player]} and now has a balance of {player.Balance}");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\n\nKeep playing? ");
                    Console.ResetColor();
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya" || answer == "yea")
                        player.isActivelyPlaying = true;
                    else
                        player.isActivelyPlaying = false;
                }
                return;
            }
            

            foreach (Player player in Players)
            {
                bool? playerWon = TwentyOneRules.CompareHands(player.Hand, Dealer.Hand);
                if (playerWon == null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Push!  No one wins.");
                    Console.ResetColor();
                    Console.WriteLine($"Dealer had {TwentyOneRules.DealerCardValue(Dealer.Hand)} and {player.Name} had {TwentyOneRules.PlayerCardValue(player.Hand)}");
                    player.Balance += Bets[player]; // Giving players their money back
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{player.Name} still has a balance of {player.Balance}\n\n");
                    Console.ResetColor();
                }
                else if (playerWon == true)
                {
                    player.Balance += (Bets[player] * 2);
                    Dealer.Balance -= Bets[player];
                    Console.WriteLine($"Dealer had {TwentyOneRules.DealerCardValue(Dealer.Hand)} and {player.Name} had {TwentyOneRules.PlayerCardValue(player.Hand)}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{player.Name} won {Bets[player]} and now has a balance of {player.Balance}\n\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"Dealer wins {Bets[player]}");
                    Console.ResetColor();
                    Console.WriteLine($"Dealer had {TwentyOneRules.DealerCardValue(Dealer.Hand)} and {player.Name} had {TwentyOneRules.PlayerCardValue(player.Hand)}");
                    Dealer.Balance += Bets[player];
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{player.Name} now has a balance of {player.Balance}\n\n");
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Play again? ");
                Console.ResetColor();
                string answer = Console.ReadLine().ToLower();
                if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya" || answer == "yea")
                {
                    player.isActivelyPlaying = true;
                }
                else
                {
                    player.isActivelyPlaying = false;
                }
            }
        }

        // Since ListPlayers() is a virtual method from Game,
        // you must use the override keyword
        public override void ListPlayers()
        {
            Console.WriteLine("21 Players");    // I can add to this method since it's a virtual method
            base.ListPlayers(); // This line holds all the code that's in Game > ListPlayers()
        }

        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }



    }
}
