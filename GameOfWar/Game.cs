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
        private int warCount = Properties.Settings.Default.warCount;
        private int turnCount = Properties.Settings.Default.turnCount;
        private bool highCardOnly = Properties.Settings.Default.highCardOnlyWar;
        private int turns, playerCount;

        private Deck deck;
        private List<Player> players;
        private List<Card> stack; //will be used for "winning cards" and appending them to queues quickly and neatly

        public Game()
        {
            playerCount =0;

            warCount = warCount > 0 ? warCount : 3; //if warCount is 0 or less, it defaults to 3
            turnCount = turnCount > 0 ? turnCount : 50; //...defaults to 50
            highCardOnly = highCardOnly ? highCardOnly : false; //Simply defaults highCardOnly to false if it's "not true" -- is this even needed?
            Console.WriteLine("Welcome to War!");
            

            StartGame();
        }

        private void StartGame()
        {
            Console.WriteLine("\nType in how many players you would like to play with. You will be Player 1.");
            Console.Write("\n(Note: For every instance of 4 players, you will be given a new deck to increase how many cards there are.\n" +
                "All players get the same amount of cards, so some cards may be left out after the initial deck is shuffled.\n" +
                "\nPlayer Amount: ");

            while (playerCount <= 1)
            {
                try { playerCount = Convert.ToInt32(Console.In.ReadLine().Trim()); } catch (Exception ex) { Console.Out.WriteLine("Please input a valid number."); break; }

                if (playerCount < 1)
                {
                    Console.WriteLine("\nPlease input a number greater than 0.");
                    Console.Write("\nPlayer Amount: ");
                }
                if (playerCount == 1)
                {
                    Console.WriteLine("\nCongrats, you win! Now how about trying again with more than 1 player?");
                    Console.Write("\nPlayer Amount: ");
                }
            }
            players = new List<Player>();
            deck = new Deck(playerCount);
            List<Deck> decks = deck.Deal();

            for(int i=0;i<playerCount;i++)
            {
                players.Add(new Player(i+1,decks[i]));
            }

            stack = new List<Card>();
            turns = 0;

            Console.WriteLine("\nYour game is now ready!");

            deck.Shuffle();

            Upkeep();
        }

        private void Upkeep()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press a key to continue...");
            Console.ForegroundColor = ConsoleColor.White;

            if (turns >= turnCount)
            {
                Console.Clear();
                Console.WriteLine("Sorry, but this game is taking too long. There are no winners. You may try again.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press a key to continue...");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadKey();
            Console.Clear();
            turns++;

            if(turns<turnCount)
                checkPlayers();

            StartGame();
        }

        public void checkPlayers()
        {
            turns++;
            List<Player> deadPlayers = new List<Player>();
            for (int x=0;x<players.Count;x++)
            {
                if (players[x].getLibrary().Count<=0)
                {
                    if (x<=0)
                    {
                        Console.WriteLine("\nSorry, but you have been eliminated. Press any key to play again.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"\nPlayer {x+1} has been eliminated.");
                        deadPlayers.Add(players[x]);
                    }
                }
            }
            if(players.Count<=1)
            {
                Console.WriteLine("\nCongratulations! You won! If you would like, you may start a new game.");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\nPress a key to continue...");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                Console.Clear();
                StartGame();
            }
            foreach(Player p in deadPlayers)
            { players.Remove(p); }
            stack = new List<Card>();

            Console.Out.WriteLine("Current deck sizes:");
            foreach(Player p in players)
            {
                p.fightCard = null;
                p.stack.Clear();
                Console.Out.WriteLine($"Player {p.playerNumber}: {p.getLibrary().Count}");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress a key to continue...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();

            FlipCard(players);
        }

        private void FlipCard(List<Player> warPlayers)
        {
            //flip the top cards of each deck and add them to stack while dequeuing them from their decks
            //stack is the collection of all cards that would be won by a player (including in Wars)
            Card winningCard = null;
            List<Player> winningPlayers = new List<Player>();
            foreach(Player p in warPlayers)
            {
                try
                {
                    p.fightCard = p.getLibrary().Dequeue();
                    stack.Add(p.fightCard);
                    p.stack.Add(new List<Card> { p.fightCard });

                    if (winningCard is null || p.fightCard.Rank > winningCard.Rank)
                    {
                        winningPlayers.Clear();
                        winningPlayers.Add(p);
                        winningCard = p.fightCard;
                    }
                    else if (p.fightCard.Rank == winningCard.Rank)
                    {
                        winningPlayers.Add(p);
                    }
                }
                catch(InvalidOperationException ex)
                {
                    Console.WriteLine($"Player {p.playerNumber} has run out of cards and will be eliminated after this war.\n");
                }
            }

            if (winningPlayers.Count <= 1)
            {
                foreach(Player p in players)
                {
                    Console.WriteLine("Player " + p.playerNumber.ToString());
                    foreach(List<Card> pStack in p.stack)
                    {
                        foreach(Card c in pStack)
                        {
                            c.showCard();
                            Console.Write(" ");
                        }
                        Console.Write("| ");
                    }
                    Console.WriteLine("");
                }

                Console.WriteLine($"Player {winningPlayers[0].playerNumber} wins this round!");
                stack.ForEach(c => winningPlayers[0].getLibrary().Enqueue(c));
            }
            else
            {
                Console.Out.WriteLine("WAR HAS BEEN DECLARED!\n");
                if (highCardOnly)
                    War(winningPlayers);
                else
                    War(players);
            }

            checkPlayers();
            Upkeep();
        }

        private void War(List<Player> warPlayers)
        {
            //put the next {warCount} cards (EXCEPT LAST CARD OF DECK) into the stack
            //the reason that stack is a list is so we can use the "foreach" method to Enqueue the stack quickly
            //while testing, remember that in the event of a single war, the player who wins "removes" 5 cards, but wins "10 cards," resulting in a net gain of 5\
            List<Card> tempDeck;
            foreach(Player p in warPlayers)
            {
                tempDeck = new List<Card>();
                try
                {
                    for (int i = 0; i < warCount; i++)
                    {
                        tempDeck.Add(p.getLibrary().Dequeue());
                        stack.Add(tempDeck[i]);
                    }
                    p.stack.Add(tempDeck);
                }
                catch (Exception ex) { }
            }
            
            FlipCard(warPlayers);
        }
    }
}

