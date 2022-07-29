using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PlayingCards.Properties;

namespace PlayingCards
{
    internal class Card
    {
        public Suit Suit { get; set;}
        public int Number { get; set; }

        public Card(Suit suit, int number)
        {
            Suit = suit;
            Number = number;
        }
    }
}
