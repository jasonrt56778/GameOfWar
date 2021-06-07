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
		foreach (string suit in suits)
		{
			for (int i = 2; i < 15; i++)
			{
				string valueForString = i.ToString();
				if (i > 10)
				{
					if (i == 11)
					{
						valueForString = "Jack";
					}
					if (i == 12)
					{
						valueForString = "Queen";
					}
					if (i == 13)
					{
						valueForString = "King";
					}
					if (i == 14)
					{
						valueForString = "Ace";
					}
				}
				Cards.Enqueue(new Card(i, suit.ToString(), valueForString + " of " + suit));
			}
		}
		ShuffleDeck();
	}
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
		return Cards.Peek().getName();
    }

	public int getValueOfFirstCard()
    {
		return Cards.Peek().getValue();
    }

	public void ShuffleDeck()
    {
		List<Card> newCards = Cards.ToList();
		List<Card> secondList = new List<Card>();
		for(int i = 52; i > 0; i--)
        {
			System.Random random = new System.Random();
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
