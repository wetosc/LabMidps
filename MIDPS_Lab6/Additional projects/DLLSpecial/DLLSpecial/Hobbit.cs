using System;

namespace DLLSpecial
{
    public class Hobbit : IComparable, SQLObject
    {
        public string Name { get; set; }
        public uint id { get; set; }
        public string Family { get; set; }
        public string Region { get; set; }
        public uint sqlID { get; set; }

        public Hobbit()
        {
        }

        public override string ToString()
        {
            return String.Format("{0} nr. {1} with Name = {2}", "Hobbit", id, Name);
        }
        public int CompareTo(object obj)
        {
            return this.ToString().CompareTo(obj.ToString());
        }
        
        //public string sortString()
        //{
        //    return "SELECT * FROM Hobbit ORDER BY Name;";
        //}
        
        #region SQLObject
        public string select()
        {
            return "use MiddleEarth; SELECT * FROM Hobbit";
        }
        public string select2(int id)
        {
            return String.Format("use MiddleEarth; SELECT * FROM Wizard WHERE Hobbit_Friend = {0}; SELECT * FROM Elf WHERE Hobbit_Friend = {0};", id);
        }
        public string insertString()
        {
            string p1 = "Name, Region", p2 = String.Format("'{0}','{1}'", this.Name, this.Region);
            return String.Format("use MiddleEarth; INSERT INTO Hobbit ({0}) VALUES ({1});", p1, p2);
        }
        public string deleteString(int n)
        {
            return String.Format("use MiddleEarth; UPDATE Elf SET Hobbit_Friend = NULL WHERE Hobbit_Friend IN (SELECT TOP {0} id FROM Hobbit); UPDATE Wizard SET Hobbit_Friend = NULL WHERE Hobbit_Friend IN (SELECT TOP {0} id FROM Hobbit); DELETE TOP({0}) FROM Hobbit;", n);
        }
        public string deleteOneString(int id)
        {
            return String.Format("use MiddleEarth; UPDATE Elf SET Hobbit_Friend = NULL WHERE Hobbit_Friend = '{0}'; UPDATE Wizard SET Hobbit_Friend = NULL WHERE Hobbit_Friend = '{0}'; DELETE FROM Hobbit WHERE id = '{0}';", id);
        }
        public string updateFormat()
        {
            return "use MiddleEarth; UPDATE Hobbit SET Name = '{1}' WHERE id = '{0}'";
        }
        #endregion
    }
}