using System;

public class Elf : IComparable
{
    public string Name { get; set; }
    public uint id { get; set; }
    public string Category { get; set; }

    public Elf()
    {
    }
    public override string ToString()
    {
        return String.Format("{0} nr. {1} with Name = {2}", "Elf", id, Name);
    }

    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}
