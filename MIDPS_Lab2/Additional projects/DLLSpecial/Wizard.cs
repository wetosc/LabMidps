﻿using System;
using SQL;
public class Wizard : IComparable, SQLObject
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
    public string insertString()
    {
        string p1 = "Name, Color", p2 = String.Format("'{0}','{1}'", this.Name, this.Color);
        return String.Format("INSERT INTO Wizard ({0}) VALUES ({1});", p1, p2);
    }
    public string deleteString(int n)
    {
        return String.Format("DELETE FROM Master2Ring WHERE Master_ID = (SELECT TOP {0} Master_ID FROM Wizard); UPDATE Orc SET Master_ID = NULL WHERE Master_ID = (SELECT TOP {0} Master_ID FROM Wizard); DELETE TOP({0}) FROM Wizard;", n);
    }
    public string updateFormat()
    {
        return "UPDATE Wizard SET Name = '{1}' WHERE Name = '{0}'";
    }
    public string sortString()
    {
        return "SELECT * FROM Wizard ORDER BY Name;";
    }
}
