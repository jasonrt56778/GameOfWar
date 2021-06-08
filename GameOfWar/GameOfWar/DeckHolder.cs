using System;
using System.Collections.Generic;
using System.Linq;

public class DeckHolder
{
	public void PopulateDeck()
    {
		string[] suits = { "Hearts", "Clubs", "Diamonds", "Spades" };
		for (int i = 2; i < 15; i++)
		{
			string valueForString = i.ToString();
			switch(i)
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
			_deck.Enqueue(new Card(i, valueForString + " of " + suits[0]));
			_deck.Enqueue(new Card(i, valueForString + " of " + suits[1]));
			_deck.Enqueue(new Card(i, valueForString + " of " + suits[2]));
			_deck.Enqueue(new Card(i, valueForString + " of " + suits[3]));
		}
		ShuffleDeck();
	}

	private Queue<Card> _deck = new Queue<Card>();
	public Queue<Card> Deck
    {
        get { return _deck; }
        set { _deck = value; }
    }

	//maybe make these into properties
	public int DeckCount()
    {
		return _deck.Count;
    }

	public string FirstCardName()
    {
		return _deck.Peek().GetName;
    }

	public int FirstCardValue()
    {
		return _deck.Peek().GetValue;
    }

	private Random rng = new Random();
	public void ShuffleDeck()
    {
		List<Card> deckList = _deck.ToList();
		int deckLength = deckList.Count;
		while (deckLength > 1)
        {
			deckLength--;
			int randomNum = rng.Next(deckLength + 1);
			Card temp = deckList[randomNum];
			deckList[randomNum] = deckList[deckLength];
			deckList[deckLength] = temp;
        }
		_deck = new Queue<Card>(deckList);
    }
}
