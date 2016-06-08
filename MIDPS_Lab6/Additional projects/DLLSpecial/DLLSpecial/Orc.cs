using System;
namespace DLLSpecial
{
    public class Orc : IComparable, SQLObject
    {
        public uint id { get; set; }
        public float Power { get; set; }
        public string Master { get; set; }
        public uint sqlID { get; set; }
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
        
        //public string sortString()
        //{
        //    return "SELECT * FROM Orc ORDER BY Power;";
        //}

        #region SQLObject
        public string select()
        {
            return "use MiddleEarth; SELECT * FROM Orc";
        }
        public string select2(int id)
        {
            return "";
        }
        public string insertString()
        {
            string p1 = "Power", p2 = String.Format("'{0}'", this.Power);
            return String.Format("use MiddleEarth; INSERT INTO Orc ({0}) VALUES ({1});", p1, p2);
        }

        public string deleteString(int n)
        {
            return String.Format("use MiddleEarth; DELETE TOP({0}) FROM Orc;", n);
        }
        public string deleteOneString(int id)
        {
            return String.Format("use MiddleEarth; DELETE FROM Orc WHERE id ='{0}';", id);
        }
        public string updateFormat()
        {
            return "";
        }
        #endregion
    }
}