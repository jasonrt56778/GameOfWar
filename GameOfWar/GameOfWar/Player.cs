using System;
using System.Collections.Generic;

public class Player
{
	private string name;
	private Deck deck;
	public Player(string name, Deck deck)
	{
		this.name = name;
		this.deck = deck;
	}

	public int getFirstCard()
    {
		return deck.getValueOfFirstCard();
    }

	public string getName()
    {
		return name;
    }
}
