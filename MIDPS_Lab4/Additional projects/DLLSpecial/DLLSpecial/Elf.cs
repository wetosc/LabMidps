using System;
using System.Collections.Generic;
using SQL;
public class Elf : IComparable, SQLObject
{
    public string Name { get; set; }
    public uint id { get; set; }
    public string Category { get; set; }
    public uint HobbitFriend { get; set; }
    public uint sqlID { get; set; }

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
    public string insertString()
    {
        string p1 = "id, Name, Category", p2 = String.Format("'{0}','{1}','{2}'",this.sqlID, this.Name, this.Category);
        return String.Format("INSERT INTO Elf ({0}) VALUES ({1});", p1, p2);
    }
    public string insertWithFriendString()
    {
        string p1 = "Name, Category, Hobbit_Friend", p2 = String.Format("'{0}','{1}','{2}','{3}'", this.sqlID, this.Name, this.Category, this.HobbitFriend);
        return String.Format("INSERT INTO Elf ({0}) output INSERTED.ID VALUES ({1});", p1, p2);
    }
    public string deleteString(int n)
    {
        return String.Format("DELETE TOP({0}) FROM Elf;",n);
    }
    public string updateFormat()
    {
        return "UPDATE Elf SET Name = '{1}' WHERE Name = '{0}'";
    }
    public string sortString()
    {
        return "SELECT * FROM Elf ORDER BY Name;";
    }
    public string verifyHobbit()
    {
        return String.Format("SELECT COUNT(*) FROM Hobbit WHERE id = '{0}';",HobbitFriend);
    }
}
