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
            //whether or not I implement a "1 or 14" option for ace, this will be able to display it either way, not that it changes the game at all
            switch(Rank)
            {
                case 1:
                    face = "A";
                    break;
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
            //the suitColor will change if classicColors is true or false in the App.config
            switch(Suit)
            {
                case Suit.Diamonds:
                    suitColor = ConsoleColor.Red; //no classicColors check since it would be red anyways
                    face += Properties.Settings.Default.displaySuitAsSymbol ? "\u2666" : "D";
                    break;
                case Suit.Spades:
                    suitColor = Properties.Settings.Default.classicColors ? ConsoleColor.Black : ConsoleColor.Blue;
                    //suitColor = ConsoleColor.Cyan;
                    face += Properties.Settings.Default.displaySuitAsSymbol ? "\u2660" : "S";
                    break;
                case Suit.Hearts:
                    suitColor = Properties.Settings.Default.classicColors ? ConsoleColor.Red : ConsoleColor.Magenta;
                    //suitColor = ConsoleColor.Magenta;
                    face += Properties.Settings.Default.displaySuitAsSymbol ? "\u2665" : "H";
                    break;
                case Suit.Clubs:
                    suitColor = Properties.Settings.Default.classicColors ? ConsoleColor.Black : ConsoleColor.DarkCyan;
                    //suitColor = ConsoleColor.DarkCyan;
                    face += Properties.Settings.Default.displaySuitAsSymbol ? "\u2663" : "C";
                    break;
            }
            //changes the color that will be displayed for the card for quick readability of suit (not that it matters for the game)
            //colors could instead be red and black (with white background for black)
            Console.ForegroundColor = suitColor;
           // if (Convert.ToBoolean(ConfigurationManager.AppSettings["classicColors"]))
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(face); 
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
