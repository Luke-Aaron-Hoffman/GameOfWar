using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfWar
{
    class Deck
    {
        private Queue<Card> library;
        public int playerCount;

        public Deck(int players)
        {
            //if playerCount = 2-4, 2/4 = 0; 3/4 = 0; 4/4 = 1; 5/4 = 1; 6/4=1; 7/4=1; 8/4=2; 
            //If I want each set of 4 to have a deck, playerCount 4 needs 1 deck, so just subtract playerCount by 1 for the division, then add by 1 so "0" isn't a deck count
            playerCount = players;
            players = ((players - 1) / 4)+1; 
            library = new Queue<Card>();
            List<Card> tempDeck = new List<Card>();
            int aceHigh = Properties.Settings.Default.aceHigh;
            aceHigh = (aceHigh >= 1) ? 1 : 0;
            //first while loop handles how many decks there will be
            while (players > 0)
            {
                //second foreach iterates through all enums (using the enums themselves)
                foreach (Suit s in (Suit[])Enum.GetValues(typeof(Suit)))
                {
                    //third for loop iterates through all the ranks that would be in a deck
                    for (int r = 1 + aceHigh; r < 14 + aceHigh; r++)
                    {
                        library.Enqueue(new Card(s, r));
                    }
                }
                players--;
            }
        }

        public Deck(Queue<Card> deck)
        {
            library = deck;
        }

        //Shuffling is done by making a temporary List, adding all cards to the list, and then randomly adding indices of the list (and then removing those parts)
        //I considered just "shuffling" the deck on creation, but realized there may be a need to shuffle mid game
        public void Shuffle()
        {
            List<Card> tempDeck = library.ToList<Card>();
            library.Clear();
            Random rand = new Random();
            int random;
            while(tempDeck.Count>0)
            {
                random = rand.Next(tempDeck.Count);
                library.Enqueue(tempDeck[random]);
                tempDeck.RemoveAt(random);
            }
        }

        public List<Deck> Deal()
        {
            List<Deck> decks = new List<Deck>();
            Shuffle();
            Queue<Card> tempDeck;
            int interval = library.Count / playerCount;
            for( int x = 0; x < playerCount;x++ )
            {
                tempDeck = new Queue<Card>();
                for(int y = 0;y<interval;y++)
                {
                    tempDeck.Enqueue(library.Dequeue());
                }
                decks.Add(new Deck(tempDeck));
            }
            return decks;
        }

        public void setLibrary(Queue<Card> deck)
        { library = deck; }

        public Queue<Card> getLibrary()
        { return library; }

    }
}
