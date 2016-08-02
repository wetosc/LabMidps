using System;
using System.Collections.Generic;
using System.Xml;
namespace DLLSpecial
{
    public class Ring : IComparable, SQLObject, XMLObject
    {
        public uint id { get; set; }
        public string Name { get; set; }
        public object Owner { get; set; }
        public string Material { get; set; }
        public string Rune { get; set; }
        public List<uint> OwnerID { get; set; }
        public uint sqlID { get; set; }
        public Ring()
        {

        }

        public Ring(uint id, string material)
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

        //public string sortString()
        //{
        //    return "SELECT * FROM Ring ORDER BY Name;";
        //}
        public string verifyMaster()
        {
            return "use MiddleEarth; SELECT COUNT(*) FROM Wizard WHERE id = '{0}';";
        }

        #region SQLObject
        public string select()
        {
            return "use MiddleEarth; SELECT * FROM Ring";
        }
        public string select2(int id)
        {
            return String.Format("use MiddleEarth; SELECT * FROM Wizard WHERE id IN (SELECT Master_ID FROM Master2Ring WHERE Ring_ID = '{0}');", id);
        }
        public string insertString()
        {
            string p1 = "Material, Name", p2 = String.Format("'{0}','{1}'", this.Material, this.Name);
            return String.Format("use MiddleEarth; INSERT INTO Ring ({0}) output INSERTED.ID VALUES ({1})", p1, p2);
        }
        public string deleteString(int n)
        {
            return String.Format("use MiddleEarth; DELETE FROM Master2Ring WHERE Ring_ID IN (SELECT TOP {0} Ring_ID  FROM Ring); DELETE TOP({0}) FROM Ring;", n);
        }
        public string deleteOneString(int id)
        {
            return String.Format("use MiddleEarth; DELETE FROM Master2Ring WHERE Ring_ID = '{0}'; DELETE FROM Ring WHERE id = '{0}';", id);
        }
        public string updateFormat()
        {
            return "use MiddleEarth; UPDATE Ring SET Name = '{1}' WHERE id = '{0}'";
        }
        #endregion
        #region XMLObject
        public void loadXML(XmlNode node)
        {
            if (node != null)
            {
                try
                {
                    this.Material = node.SelectSingleNode("Material").InnerText;
                    this.Name = node.SelectSingleNode("Name").InnerText;
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Tăt normal, ceva lipsește");
                }
                this.OwnerID = new List<uint>();
            }
        }
        #endregion
    }
}