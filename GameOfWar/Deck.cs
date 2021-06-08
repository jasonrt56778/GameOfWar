using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
	private Queue<Card> Cards = new Queue<Card>();
	public Deck()
	{
		
	}

	public void populateDeck()
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
			Cards.Enqueue(new Card(i, valueForString + " of " + suits[0]));
			Cards.Enqueue(new Card(i, valueForString + " of " + suits[1]));
			Cards.Enqueue(new Card(i, valueForString + " of " + suits[2]));
			Cards.Enqueue(new Card(i, valueForString + " of " + suits[3]));
		}
		ShuffleDeck();
	}

	//rename things to make more sense
	public Queue<Card> getDeck()
    {
		return Cards;
    }

	public void setDeck(Queue<Card> newDeck)
    {
		Cards = newDeck;
    }

	public int deckCount()
    {
		return Cards.Count;
    }

	public string getNameOfFirstCard()
    {
		return Cards.Peek().getName;
    }

	public int getValueOfFirstCard()
    {
		return Cards.Peek().getValue;
    }

	public void ShuffleDeck()
    {
		List<Card> newCards = Cards.ToList();
		List<Card> secondList = new List<Card>();
		System.Random random = new System.Random();
		for (int i = 52; i > 0; i--)
        {
			int randomNum = random.Next(i);
			secondList.Add(newCards[randomNum]);
			newCards.RemoveAt(randomNum);
        }
		Queue<Card> tempQueue = new Queue<Card>();
		foreach(Card x in secondList)
        {
			tempQueue.Enqueue(x);
        }
		Cards = tempQueue;
    }
}
