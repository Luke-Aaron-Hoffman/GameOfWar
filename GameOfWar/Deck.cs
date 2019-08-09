using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfWar
{
    class Deck
    {
        public Queue<Card> library;

        public Deck()
        {
            library = new Queue<Card>();
            List<Card> tempDeck = new List<Card>();
            int aceHigh = Properties.Settings.Default.aceHigh;
            aceHigh = (aceHigh >= 1) ? 1 : 0;
            //first foreach iterates through all enums (using the enums themselves)
            foreach(Suit s in (Suit[])Enum.GetValues(typeof(Suit)))
            {
                //second for loop iterates through all the ranks that would be in a deck
                for(int r = 1+aceHigh; r<14+aceHigh; r++)
                {
                    library.Enqueue(new Card(s, r));
                }
            }
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

        public void setLibrary(IEnumerable<Card> deck)
        { library = (Queue<Card>)deck; }

        public Queue<Card> getLibrary()
        { return library; }

    }
}
