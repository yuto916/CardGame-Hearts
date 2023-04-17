using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Hearts
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }


    public enum Value
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }



    
    /* *************************************************** CardDeck class *************************************************** */
    public class CardDeck
    {
        // Declare variable(s)
        public List<Card> cards;
        private Dictionary<string, string> cardImages;


        public CardDeck()
        {
            cards = new List<Card>();

            // Generate a standard deck of 52 cards
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    cards.Add(new Card(suit, value));
                }
            }


            // Populate the cardImages dictionary with image filenames
            cardImages = new Dictionary<string, string>();
            foreach (Card card in cards)
            {
                string key = card.ToString();
                string value = $"{(int)card.Value}{card.Suit.ToString()[0]}.gif";
                cardImages.Add(key, value);
            }
        }


        public void Shuffle()
        {
            Random random = new Random();

            for (int i = 0; i < cards.Count; i++)
            {
                int randomIndex = random.Next(cards.Count);
                Card temp = cards[i];
                cards[i] = cards[randomIndex];
                cards[randomIndex] = temp;
            }
        }


        public Card Draw()
        {
            if (cards.Count == 0)
            {
                throw new InvalidOperationException("Deck is empty");
            }

            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }


        

    public string GetCardImage(Card card)
        {
            if (!cardImages.ContainsKey(card.ToString()))
            {
                throw new ArgumentException("Card not found in image dictionary");
            }

            return cardImages[card.ToString()];
        }
    }
    /* ********************************************************************************************************************** */





    /* ******************************************************** Card  ******************************************************* */
    public class Card : IComparable<Card>
    {
        public Suit Suit { get; }
        public Value Value { get; }

        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }

        public int CompareTo(Card other)
        {
            if (other == null) return 1;
            if (this.Value > other.Value) return 1;
            if (this.Value < other.Value) return -1;
            return 0;
        }
    }
    /* ********************************************************************************************************************** */



   
    
}
