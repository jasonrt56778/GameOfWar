using System;
using System.Collections.Generic;
using System.Linq;
namespace GameOfWar
{
	public class DeckHolder
	{
		//creates a deck of 52 cards then shuffles it
		public void PopulateDeck()
		{
			for (int i = 2; i < 15; i++)
			{
				string valueForString = i.ToString();
				switch (i)
				{
					case 11:
						valueForString = "Jack";
						break;
					case 12:
						valueForString = "Queen";
						break;
					case 13:
						valueForString = "King";
						break;
					case 14:
						valueForString = "Ace";
						break;
				}
                _deck.Enqueue(new Card(i, valueForString + " of Hearts"));
				_deck.Enqueue(new Card(i, valueForString + " of Clubs"));
				_deck.Enqueue(new Card(i, valueForString + " of Diamonds"));
				_deck.Enqueue(new Card(i, valueForString + " of Spades"));
			}
			ShuffleDeck();
		}

		//creates a Queue that acts as the Deck for each player
		private Queue<Card> _deck = new Queue<Card>();
		public Queue<Card> Deck
		{
			get { return _deck; }
			set { _deck = value; }
		}

		//returns how many cards are in the Queue
		public int DeckCount
		{
			get { return _deck.Count; }
		}

		//returns the name of the first card in the deck
		public string FirstCardName
		{
			get { return _deck.Peek().GetName; }
		}

		//returns the value of the first card in the deck
		public int FirstCardValue
		{
			get { return _deck.Peek().GetValue; }
		}

		//Shuffles the deck using Fisher–Yates shuffle
		private Random rng = new Random();
		public void ShuffleDeck()
		{
			List<Card> deckList = _deck.ToList();
			int deckLength = deckList.Count;
			while (deckLength > 1)
			{
				deckLength--;
				int randomNum = rng.Next(deckLength);
				Card temp = deckList[randomNum];
				deckList[randomNum] = deckList[deckLength];
				deckList[deckLength] = temp;
			}
			_deck = new Queue<Card>(deckList);
		}
	}
}
