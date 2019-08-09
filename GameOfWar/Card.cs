using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfWar
{
    //Should suits be ints for any reason? Don't believe so, only check I'll use will be for text color if I can, and for displaying the name I can just take the first char of the string.
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
    public class Card
    {
        public Suit Suit { get; set; }
        public int Rank { get; set; }

        public Card(Suit suit, int rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public void showCard()
        {
            string face;
            ConsoleColor suitColor = ConsoleColor.White;

            //change any rank higher than 10 into its coresponding letter
            switch(Rank)
            {
                case 11:
                    face = "J";
                    break;
                case 12:
                    face = "Q";
                    break;
                case 13:
                    face = "K";
                    break;
                case 14:
                    face = "A";
                    break;
                default:
                    face = Rank.ToString();
                    break;
            }

            //Change the color of the "card" to match its suit
            switch(Suit)
            {
                case Suit.Diamonds:
                    suitColor = ConsoleColor.Red;
                    face += "\u2666";
                    break;
                case Suit.Spades:
                    suitColor = ConsoleColor.Cyan;
                    face += "\u2660";
                    break;
                case Suit.Hearts:
                    suitColor = ConsoleColor.Magenta;
                    face += "\u2665";
                    break;
                case Suit.Clubs:
                    suitColor = ConsoleColor.DarkCyan;
                    face += "\u2663";
                    break;
            }
            //changes the color that will be displayed for the card for quick readability of suit (not that it matters for the game)
            //colors could instead be red and black (with white background for black)
            Console.ForegroundColor = suitColor; 
            Console.Write(face); 
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
