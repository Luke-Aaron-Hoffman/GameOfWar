using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfWar
{
    class Player
    {
        public int playerNumber { get; set; }
        public Deck deck { get; set; }
        public Card fightCard { get; set; }
        public List<List<Card>> stack { get; set; }

        public Player(int num, Deck d)
        {
            playerNumber = num;
            deck = d;
            stack = new List<List<Card>>();
        }

        public Queue<Card> getLibrary()
        {
            return deck.getLibrary();
        }
    }
}
