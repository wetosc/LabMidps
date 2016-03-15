using System;

public class Wizard : IComparable
{
    public string Name { get; set; }
    public uint id { get; set; }
    public string Color { get; set; }

    public Wizard()
	{
	}
    public override string ToString()
    {
        return String.Format("{0} nr. {1} with Name = {2}", "Wizard", id, Name);
    }
    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}
