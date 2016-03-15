using System;

public class Hobbit: IComparable
{
    public string Name { get; set; }
    public uint id { get; set; }
    public string Family { get; set; }
    public string Region { get; set; }

    public Hobbit()
	{
	}

    public override string ToString()
    {
        return String.Format("{0} nr. {1} with Name = {2}","Hobbit", id, Name);
    }
    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}
