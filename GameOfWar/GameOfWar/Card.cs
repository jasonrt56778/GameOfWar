using System;

public class Card
{
	//card object holds the value of the card and the name
	public Card(int Value, string Name)
	{
		_value = Value;
		_name = Name;
	}
	//returns the value of the card
	private int _value;
	public int GetValue
	{
		get { return _value; }
	}
	//returns the name of the card
	private string _name;
	public string GetName
	{
		get { return _name; }
	}
}
