using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Casino;
using Casino.TwentyOne;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {




            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Welcome to the Grand Hotel and Casino.\nLet's start by telling me your name: ");
            Console.ResetColor();
            string playerName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("And how much money did you bring today? ");
            Console.ResetColor();
            int bank = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"Hello {playerName}, would you like to join a game of 21, right now? ");
            Console.ResetColor();
            string answer = Console.ReadLine().ToLower();

            // Check if they want to play
            if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya" || answer == "yea")
            {
                // If they want to play, create a new player object
                Player player = new Player(name: playerName, beginningBalance: bank);
                // Now that a player has been created, create a new game
                Game game = new TwentyOneGame();
                // Add player to the game
                game += player;
                player.isActivelyPlaying = true;
                // While the player still wants to play and has enough money
                while (player.isActivelyPlaying && player.Balance > 0)
                {
                    game.Play();
                    if (player.Balance == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("You do not have enough money to continue.");
                        Console.ResetColor();
                    }
                }
                // If the player exits the game, we need to remove the player from the game
                game -= player;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Thank you for playing!");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Feel free to look around the casino.  Bye for now.");
            Console.ResetColor();

            Console.ReadKey();
        }

        
    }
    
}











// ****** DATETIME - TIMESPAN ******
//DateTime yearOfBirth = new DateTime(1984, 4, 10, 7, 11, 32);
//DateTime yearOfGraduation = new DateTime(2002, 6, 7, 13, 00, 00);

//TimeSpan ageAtGraduation = yearOfGraduation - yearOfBirth;


// ****** INPUT OUTPUT ******
//string text = "Here is some text."; // This is the text it's going to write out
//File.WriteAllText(@"E:\Coding\Visual Studio\Projects\The Tech Academy\TwentyOneGame\log.txt", text);
// This is reading the log.txt file and storing the words into text1
//string text1 = File.ReadAllText(@"E:\Coding\Visual Studio\Projects\The Tech Academy\TwentyOneGame\log.txt");


// ****** LAMBDA EXPRESSIONS ******
//Deck deck = new Deck();

// ****** LAMBDA COUNT ******
// x represents each element in the list
// element where element.Face equals Face.Ace 
//int count = deck.Cards.Count(x => x.Face == Face.Ace);
// Console.WriteLine(count);
// OUTPUT 4 

// ****** LAMBDA WHERE ******
// Take the list of cards and find out where the Face equals King and map that to a new List
//List<Card> newList = deck.Cards.Where(x => x.Face == Face.King).ToList();   // Instead of using 'new' keyword, you have to use .ToList()

//foreach (Card card in newList)
//{
//    Console.Write(card.Face + " ");
//}
// OUTPUT KING KING KING KING

//List<int> numberList = new List<int>() { 1, 2, 3, 535, 342, 23 };
//int sum = numberList.Sum();
//Console.WriteLine(sum); // OUTPUT 906
//int sum1 = numberList.Sum(x => x + 5);  // Add 5 to each element and then sum it
//Console.WriteLine(sum1); // OUTPUT 936
//int max = numberList.Max(); // Grab the maximum number in the list
//Console.WriteLine(max); // OUTPUT 535
//int sum2 = numberList.Where(x => x > 20).Sum(); // Grab all the numbers that are greater than 20 and add them up
//Console.WriteLine(sum2); // OUTPUT 900



// ****** STRUCTS ******
//Card card1 = new Card(); // New instances for Structs go on the stack
//Card card2 = card1; // All this does is give the address of the memory location of card1
//card1.Face = Face.Eight;
//card2.Face = Face.King;

//Console.WriteLine(card1.Face);  // Returns Eight


// ****** CLASSES ******
// All Classes are by type Reference
//Card card1 = new Card(); // New instances for Classes go on the heap
//Card card2 = card1; // All this does is give the address of the memory location of card1
//card1.Face = Face.Eight;
//card2.Face = Face.King;

//Console.WriteLine(card1.Face);  // Returns King




// ****** USING ENUMS ******
//Card card = new Card();
//card.Suit = Suit.Clubs;
//int underlyingValue = (int)Suit.Diamonds; // Casting string Diamonds into an int.  Could have used Convert.ToInt32(Suit.Diamonds); as well
//Console.WriteLine(underlyingValue); // Output is 1.  Look at Card.cs > enum Suit.  It's zero based unless otherwise noted
