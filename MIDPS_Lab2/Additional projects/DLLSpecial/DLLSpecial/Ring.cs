using System;
using System.Collections.Generic;
using SQL;
public class Ring : IComparable, SQLObject
{
    public uint id { get; set; }
    public string Name { get; set; }
    public object Owner { get; set; }
    public string Material { get; set; }
    public string Rune { get; set; }
    public List<uint> OwnerID { get; set; }

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

    public string insertString()
    {
        string p1 = "Material, Name", p2 = String.Format("'{0}','{1}'", this.Material, this.Name);
        return String.Format("INSERT INTO Ring ({0}) output INSERTED.ID VALUES ({1})", p1, p2);
    }

    public string deleteString(int n)
    {
        return String.Format("DELETE FROM Master2Ring WHERE Ring_ID IN (SELECT TOP {0} Ring_ID  FROM Ring); DELETE TOP({0}) FROM Ring;",n);
    }
    public string updateFormat()
    {
        return "UPDATE Ring SET Name = '{1}' WHERE Name = '{0}'";
    }
    public string sortString()
    {
        return "SELECT * FROM Ring ORDER BY Name;";
    }
    public string verifyMaster()
    {
        return "SELECT COUNT(*) FROM Wizard WHERE id = '{0}';";
    }
}
