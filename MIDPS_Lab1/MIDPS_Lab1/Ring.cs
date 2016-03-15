using System;

public class Ring : IComparable
{
    public uint id { get; set; }
    public string Name { get; set; }
    public object Owner { get; set; }
    public string Material { get; set; }
    public string Rune { get; set; }

    public Ring()
    {

    }

    public Ring(uint id,  string material)
    {
        this.id = id;
        this.Material = material;
    }
    public override string ToString()
    {
        return String.Format("{0} nr. {1} with Name = {2}", "Ring", id, Name);
    }
    public int CompareTo(object obj)
    {
        return this.ToString().CompareTo(obj.ToString());
    }
}
