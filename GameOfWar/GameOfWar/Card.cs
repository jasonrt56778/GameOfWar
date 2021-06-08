using System;

public class Card
{
	public Card(int Value, string Name)
	{
		_value = Value;
		_name = Name;
	}

	private int _value;
	public int GetValue
	{
        get { return _value; }
        set { _value = value; }
	}

	private string _name;
	public string GetName
	{
		get { return _name; }
        set { _name = value; }
	}
}
