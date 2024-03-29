﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfWar
{
    //Starting Off Notes: I want a Config file, but it's not necessary to implement at first for this at least.
    //What values for config: InfiniteTurnCount (int) | AceHigh (boolean) [True = Ace > King; False = Ace < 2] | ClassicColors (boolean) [True = Red/Black; False = 4 Color]
    //WarCount (int) [Amount of cards pulled during a war (not counting the card that actually fights)] | StackOnTop (boolean) [True = won cards go on top; False = won cards go on bottom]
    //InfiniteTurnCount is necessary, AceHigh is nice and simple, ClassicColors is a bit much and totally unnecessary
    //WarCount is a nice QOL feature, StackOnTop is nice since it's subjective where cards go. Alternatively, this could be a 3-way value that includes a "you pick" option. Save that for later.

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            new Game();
        }
    }
}
