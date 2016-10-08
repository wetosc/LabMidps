using System;
using System.Text;
using System.Xml;

namespace DLLSpecial
{
    public class Elf : IComparable, SQLObject, XMLObject
    {
        public string Name { get; set; }
        public uint id { get; set; }
        public string Category { get; set; }
        public uint HobbitFriend { get; set; }
        public uint sqlID { get; set; }
        public byte[] imageData;

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


        //public string insertString()
        //{
        //    string p1 = "id, Name, Category", p2 = String.Format("'{0}','{1}','{2}'", this.sqlID, this.Name, this.Category);
        //    return String.Format("INSERT INTO Elf ({0}) VALUES ({1});", p1, p2);
        //}

        //public string sortString()
        //{
        //    return "SELECT * FROM Elf ORDER BY Name;";
        //}

        public string verifyHobbit()
        {
            if (HobbitFriend > 0)
            {
                return String.Format("use MiddleEarth; SELECT COUNT(*) FROM Hobbit WHERE id = '{0}';", HobbitFriend);
            }
            else return "";
        }

        public string insertWithoutFriendString()
        {
            string p1 = "Name, Category, Image", p2 = String.Format("'{0}','{1}' ,@Image ", this.Name, this.Category);
            return String.Format("use MiddleEarth; INSERT INTO Elf ({0}) output INSERTED.ID VALUES ({1});", p1, p2);
        }

        #region SQLObject
        public string select()
        {
            return "use MiddleEarth; SELECT * FROM Elf";
        }
        public string select2(int id)
        {
            return "";
        }
        public string insertString()
        {
            string p1 = "Name, Category, Hobbit_Friend, Image", p2 = String.Format("'{0}','{1}','{2}',@Image ", this.Name, this.Category, this.HobbitFriend);
            return String.Format("use MiddleEarth; INSERT INTO Elf ({0}) output INSERTED.ID VALUES ({1});", p1, p2);
        }
        public string deleteString(int n)
        {
            return String.Format("use MiddleEarth; DELETE TOP({0}) FROM Elf;", n);
        }
        public string deleteOneString(int id)
        {
            return String.Format("use MiddleEarth; DELETE FROM Elf WHERE id ='{0}';", id);
        }
        public string updateFormat()
        {
            return "use MiddleEarth; UPDATE Elf SET Name = '{1}' WHERE id = '{0}'";
        }
        #endregion


        #region XMLObject
        public void loadXML(XmlNode node)
        {
            if (node != null)
            {
                try
                {
                    this.Name = node.SelectSingleNode("Name").InnerText;
                    this.Category = node.SelectSingleNode("Category").InnerText;
                    this.HobbitFriend = uint.Parse(node.SelectSingleNode("Hobbit_Friend").InnerText);
                    this.imageData = Encoding.ASCII.GetBytes(node.SelectSingleNode("Image").InnerText);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Tăt normal, ceva lipsește");
                }
            }
        }
        #endregion

    }
}