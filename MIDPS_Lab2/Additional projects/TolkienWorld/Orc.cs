using System;

public class Orc : IComparable
{
    public uint id { get; set; }
    public float Power { get; set; }
    public string Master { get; set; }

    public Orc()
    {

    }
    public override string ToString()
    {
        return String.Format("{0} nr. {1} with Master = {2}", "Orc", id, Master);
    }
    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}
