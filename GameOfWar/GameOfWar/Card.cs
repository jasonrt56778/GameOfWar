using System;

public class Card
{
	private int Value;
	private string Suit;
	private string Name;
	public Card(int Value, string Suit, string Name)
	{
		this.Value = Value;
		this.Suit = Suit;
		this.Name = Name;
	}
	
	public int getValue()
    {
		return Value;
    }
	protected string getSuit()
	{
		return Suit;
	}
	public string getName()
	{
		return Name;
	}
}
