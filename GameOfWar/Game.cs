using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfWar
{
    //Initially there was no Game class, and it was all handled in the main Program, but it became too cumbersome
    class Game
    {
        private const int warCount = 3;
        private const int turnCount = 1000;
        private int turns;

        private Deck deck;
        private Deck player1;
        private Deck player2;
        private List<Card> stack; //will be used for "winning cards" and appending them to queues quickly and neatly

        public Game()
        {
            Console.Out.WriteLine("Welcome to War!");
            Console.Out.WriteLine("\nAll you have to do is press any key to keep proceeding through the game. You will be Player 1, and the Computer will be Player 2. Good luck!");

            StartGame();
        }

        private void StartGame()
        {
            deck = new Deck();
            int deckSize = deck.getLibrary().Count;
            player1 = new Deck();
            player2 = new Deck();
            stack = new List<Card>();
            turns = 0;

            deck.Shuffle();

            Queue<Card> deck1 = new Queue<Card>();
            Queue<Card> deck2 = new Queue<Card>();

            //pops out the first {getLibrary.Count/2} cards and puts them into deck1 (which will be assiend to player1)
            for (int x = 0; x < deckSize/2; x++)
            {
                deck1.Enqueue(deck.getLibrary().Dequeue());
            }
            player1.setLibrary(deck1);
            //pops out the last {getLibrary.Count/2} cards. This could cause errors if the deck size is odd, but there should never be an instance like that
            for (int x = 0; x < deckSize/2; x++)
            {
                deck2.Enqueue(deck.getLibrary().Dequeue());
            }
            player2.setLibrary(deck2);

            Upkeep();
        }

        private void Upkeep()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Out.WriteLine("Press a key to continue...");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
            Console.Clear();
            turns++;

            if(player1.getLibrary().Count<=0)
            {
                Console.Out.WriteLine("Sorry, but you have lost. Press any key to play again.");
            }
            else if(player2.getLibrary().Count<=0)
            {
                Console.Out.WriteLine("Congratulations, you have won! Press any key to play again.");
            }
            else if(turns>turnCount)
            {
                Console.Out.WriteLine("Unfortunately this game is too long, let's restart. Press any key to play again.");
            }
            else
            {
                Console.WriteLine("Player1\t\tPlayer2");
                stack = new List<Card>();
                FlipCard();
            }

            StartGame();
        }

        private void FlipCard()
        {
            Card card1, card2;

            card1 = player1.getLibrary().Dequeue();
            card1.showCard();
            Console.Write("\t\t");
            card2 = player2.getLibrary().Dequeue();
            card2.showCard();
            Console.WriteLine();

            stack.Add(card1);
            stack.Add(card2);

            if (card1.Rank > card2.Rank)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Player 1 wins this round!");
                Console.ForegroundColor = ConsoleColor.White;
                stack.ForEach(c => player1.getLibrary().Enqueue(c));
            }
            else if (card1.Rank < card2.Rank)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Player 2 wins this round!");
                Console.ForegroundColor = ConsoleColor.White;
                stack.ForEach(c => player2.getLibrary().Enqueue(c));
            }
            //check if either player has no cards (cannot do a war with 0 cards)
            else if(player1.getLibrary().Count*player2.getLibrary().Count != 0)
            {
                //Have to be careful if there's an empty deck (handle that exception) NOTE: Just check for "count == 1" instead and then DONT do the thing\
                //What happens during a tie AND NO MORE CARDS?
                //No official ruling, will thus assume that if starting a new war and the player runs out of cards, they'll use the last card drawn, but if that results in a tie they will lose
                Console.Out.WriteLine("WAR HAS BEEN DECLARED!");
                War();
            }
            //This just displays who ran out of cards during a war
            //in particular, this else statement happens when the currently pulled cards are equal AND someone's deck is empty
            else
            {
                String player = (player1.getLibrary().Count > 0) ? "Player 2" : "Player 1";
                Console.Out.WriteLine($"{player} has run out of cards and is unable to commence war.");
            }

            Console.WriteLine($"P1: {player1.getLibrary().Count} cards\tP2: {player2.getLibrary().Count} cards");
            Upkeep();
        }

        private void War()
        {
            //put the next {warCount} cards (EXCEPT LAST CARD OF DECK) into the stack
            //the reason that stack is a list is so we can use the "foreach" method to Enqueue the stack quickly
            //while testing, remember that in the event of a single war, the player who wins "removes" 5 cards, but wins "10 cards," resulting in a net gain of 5
            for (int x = 0; x < warCount; x++)
            {
                if (player1.getLibrary().Count > 1)
                {
                    Card c = player1.getLibrary().Dequeue();
                    c.showCard(); Console.Write(" ");
                    stack.Add(c);
                }
            }
            Console.Write("\t");
            for (int x = 0; x < warCount; x++)
            {
                if (player2.getLibrary().Count > 1)
                {
                    Card c = player2.getLibrary().Dequeue();
                    c.showCard(); Console.Write(" ");
                    stack.Add(c);
                }
            }
            Console.WriteLine("");
            FlipCard();
        }
    }
}

