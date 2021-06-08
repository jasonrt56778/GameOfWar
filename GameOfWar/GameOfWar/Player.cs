using System;
using System.Collections.Generic;

public class Player
{
	private string name;
	private DeckHolder deck;
	public Player(string name, DeckHolder deck)
	{
		this.name = name;
		this.deck = deck;
	}
}
