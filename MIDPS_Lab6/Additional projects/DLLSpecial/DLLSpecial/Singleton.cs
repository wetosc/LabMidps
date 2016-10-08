using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

namespace DLLSpecial
{
    public interface SQLObject
    {
        string select();
        string select2(int id);
        string insertString();
        string deleteString(int n);
        string updateFormat();
        string deleteOneString(int id);
    }

    public interface XMLObject
    {
        void loadXML(XmlNode node);
    }


    public class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton() { }

        private string connectionString = "Data Source=DESKTOP-FNFDKC7\\SQLEXPRESS;Initial Catalog=MiddleEarth;Integrated Security=True";
        private string connectionStringUsers = "Data Source=DESKTOP-FNFDKC7\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True";



        private Singleton()
        {
            connection = new SqlConnection(connectionString);
        }

        public static Singleton Instance
        {
            get
            {
                return instance;
            }
        }

        public SqlConnection connection { get; set; }

        public void start()
        {
            try
            {
                connect();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void connect()
        {
            if (connection.State == System.Data.ConnectionState.Broken || connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public DataSet getData(Type t)
        {
            connect();

            SQLObject obj = (SQLObject)Activator.CreateInstance(t);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = connection;
            sqlCmd.CommandText = obj.select();
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
            DataSet dtRecord = new DataSet();
            sqlDataAdap.Fill(dtRecord);
            return dtRecord;
        }
        public DataSet getData2(Type t, int id)
        {
            connect();

            SQLObject obj = (SQLObject)Activator.CreateInstance(t);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = connection;
            sqlCmd.CommandText = obj.select2(id);
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
            DataSet dtRecord = new DataSet();
            sqlDataAdap.Fill(dtRecord);
            return dtRecord;
        }

        public byte[] getImage(int id)
        {
            connect();

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = connection;
            sqlCmd.CommandText = String.Format("SELECT Image from Elf WHERE id = '{0}'", id);
            object data = sqlCmd.ExecuteScalar();

            return (data != null) ? (data as byte[]) : null;
        }

        public int Insert(SQLObject temp, string type)
        {
            connect();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            switch (type)
            {
                case "Ring":
                    {
                        cmd.CommandText = temp.insertString();
                        int mID = (int)cmd.ExecuteScalar();
                        if (mID > 0)
                        {
                            foreach (uint id in ((Ring)temp).OwnerID)
                            {
                                cmd.CommandText = String.Format(((Ring)temp).verifyMaster(), id);
                                if ((int)cmd.ExecuteScalar() > 0)
                                {
                                    cmd.CommandText = String.Format("INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES ('{0}','{1}');", mID, id);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            return 1;
                        }
                        break;
                    }
                case "Wizard":
                    {
                        cmd.CommandText = temp.insertString();
                        int mID = (int)cmd.ExecuteScalar();
                        if (mID > 0)
                        {
                            foreach (uint id in ((Wizard)temp).RingID)
                            {
                                cmd.CommandText = String.Format(((Wizard)temp).verifyRing(), id);
                                if ((int)cmd.ExecuteScalar() > 0)
                                {
                                    cmd.CommandText = String.Format("INSERT INTO Master2Ring (Ring_ID,Master_ID) VALUES ('{0}','{1}');", id, mID);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            return 1;
                        }
                        break;
                    }
                case "Elf":
                    {
                        cmd.CommandText = ((Elf)temp).verifyHobbit();
                        if (cmd.CommandText.Length > 0 && (int)cmd.ExecuteScalar() > 0)
                        {
                            cmd.CommandText = ((Elf)temp).insertString();
                            cmd.Parameters.Add("@Image", SqlDbType.Image);
                            cmd.Parameters["@Image"].Value = (temp as Elf).imageData;
                            if ((temp as Elf).imageData == null) cmd.Parameters["@Image"].Value = DBNull.Value;
                            return cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = (temp as Elf).insertWithoutFriendString();
                            cmd.Parameters.Add("@Image", SqlDbType.Image);
                            cmd.Parameters["@Image"].Value = (temp as Elf).imageData;
                            if ((temp as Elf).imageData == null) cmd.Parameters["@Image"].Value = DBNull.Value;
                            return cmd.ExecuteNonQuery();
                        }
                        break;
                    }
                default:
                    cmd.CommandText = temp.insertString();
                    return cmd.ExecuteNonQuery();
            }

            return 0;
        }

        public int deleteOne(Type t, int id)
        {
            connect();

            SqlCommand cmd = new SqlCommand();
            SQLObject obj = (SQLObject)Activator.CreateInstance(t);
            cmd.CommandText = obj.deleteOneString(id);
            cmd.Connection = connection;
            return cmd.ExecuteNonQuery();
        }

        public int deleteMultiple(Type t, int n)
        {
            connect();

            SqlCommand cmd = new SqlCommand();
            SQLObject obj = (SQLObject)Activator.CreateInstance(t);
            cmd.CommandText = obj.deleteString(n);
            cmd.Connection = connection;
            return cmd.ExecuteNonQuery();
        }

        public int update(Type t, int id, string newName)
        {
            connect();
            SqlCommand cmd = new SqlCommand();
            SQLObject obj = (SQLObject)Activator.CreateInstance(t);
            cmd.CommandText = String.Format(obj.updateFormat(), id, newName);
            cmd.Connection = connection;
            return cmd.ExecuteNonQuery();
        }

        public int updateImage(int id, byte[] data)
        {
            connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format("UPDATE Elf SET Image = @Image WHERE id = '{0}'", id);
            cmd.Connection = connection;
            cmd.Parameters.Add("@Image", SqlDbType.Image);
            cmd.Parameters["@Image"].Value = data;
            if (data == null) cmd.Parameters["@Image"].Value = DBNull.Value;
            return cmd.ExecuteNonQuery();
        }

        public DataSet rightsForUser(string userName, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionStringUsers))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Users WHERE UserName = @userName AND Password = @password";
                cmd.Parameters.Add("@userName", SqlDbType.VarChar);
                cmd.Parameters.Add("@password", SqlDbType.VarChar);
                cmd.Parameters["@userName"].Value = userName;
                cmd.Parameters["@password"].Value = password;
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(cmd);
                DataSet dtRecord = new DataSet();
                sqlDataAdap.Fill(dtRecord);
                return dtRecord;
            }
        }

        public DataSet getUserList()
        {
            using (SqlConnection conn = new SqlConnection(connectionStringUsers))
            {

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = conn;
                sqlCmd.CommandText = "SELECT * FROM Users";
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
                DataSet dtRecord = new DataSet();
                sqlDataAdap.Fill(dtRecord);
                return dtRecord;
            }
        }

        public void saveToXML()
        {
            connect();

            string tempFileName = "temp.xml", finalFileName = "final.xml";
            List<SQLObject> objs = new List<SQLObject> { new Hobbit(), new Wizard(), new Orc(), new Elf(), new Ring() };
            XmlDocument root = new XmlDocument();
            var parent = root.CreateElement("root");

            foreach (SQLObject obj in objs)
            {
                SqlCommand sqlCmd = new SqlCommand(obj.select(), connection);
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
                DataSet dtRecord = new DataSet();
                sqlDataAdap.Fill(dtRecord);
                dtRecord.WriteXml(tempFileName);

                XmlDocument docTemp = new XmlDocument(); docTemp.Load(tempFileName);
                XmlNode el = docTemp.SelectSingleNode("/NewDataSet");
                XmlElement elem = docTemp.CreateElement(obj.GetType().ToString());
                if (el != null) { elem.AppendChild(el); }
                XmlNode nod = root.ImportNode(elem, true);
                parent.AppendChild(nod);
            }
            using (SqlCommand sqlCmd = new SqlCommand("use MiddleEarth; SELECT * FROM Master2Ring", connection))
            {

                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);
                DataSet dtRecord = new DataSet();
                sqlDataAdap.Fill(dtRecord);
                dtRecord.WriteXml(tempFileName);

                XmlDocument docTemp = new XmlDocument(); docTemp.Load(tempFileName);
                XmlNode el = docTemp.SelectSingleNode("/NewDataSet");
                XmlElement elem = docTemp.CreateElement("DLLSpecial.Master2Ring");
                if (el != null) { elem.AppendChild(el); }
                XmlNode nod = root.ImportNode(elem, true);
                parent.AppendChild(nod);
            }
            root.AppendChild(parent);
            root.Save(finalFileName);
        }

        public void delete_create()
        {
            connect();
            string sqlString = "use MiddleEarth;     DROP TABLE Master2Ring;   DROP TABLE Ring;   DROP TABLE Orc;   DROP TABLE Wizard;   DROP TABLE Elf;   DROP TABLE Hobbit;     CREATE TABLE Hobbit (     id INTEGER IDENTITY NOT NULL,     Name VARCHAR(256) NULL DEFAULT NULL,     Region VARCHAR(256) NULL DEFAULT NULL,     PRIMARY KEY (id)   );      CREATE TABLE Elf (     id INTEGER IDENTITY NOT NULL,     Name VARCHAR(256) NULL DEFAULT NULL,     Category VARCHAR(256) NULL DEFAULT NULL,     Hobbit_Friend INTEGER NULL DEFAULT NULL,     Image image NULL,     PRIMARY KEY (id),     CONSTRAINT  Hobbit_Friend_FK2 FOREIGN KEY (Hobbit_Friend) REFERENCES Hobbit (id)   );      CREATE TABLE Ring (     id INTEGER IDENTITY NOT NULL,     Material VARCHAR(256) NULL DEFAULT NULL,     Name VARCHAR(256) NULL DEFAULT NULL,     PRIMARY KEY (id)   );      CREATE TABLE Wizard (     id INTEGER IDENTITY NOT NULL,     Name VARCHAR(256) NULL DEFAULT NULL,     Color VARCHAR(256) NULL DEFAULT NULL,     Hobbit_Friend INTEGER NULL DEFAULT NULL,     PRIMARY KEY (id),     CONSTRAINT Hobbit_Friend_FK FOREIGN KEY (Hobbit_Friend) REFERENCES Hobbit (id)   );      CREATE TABLE Master2Ring (     Ring_ID INTEGER NULL DEFAULT NULL,     Master_ID INTEGER NULL DEFAULT NULL,     CONSTRAINT  Ring_ID_FK FOREIGN KEY (Ring_ID) REFERENCES Ring (id),     CONSTRAINT  Master_ID_FK FOREIGN KEY (Master_ID) REFERENCES Wizard (id)   );         CREATE TABLE Orc (     id INTEGER IDENTITY NOT NULL,     Power FLOAT NULL DEFAULT NULL,     Master_ID INTEGER NULL DEFAULT NULL,     PRIMARY KEY (id),     CONSTRAINT  Master_ID_FK2 FOREIGN KEY (Master_ID) REFERENCES Wizard (id)   );";
            SqlCommand cmd = new SqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
        }


        public void loadFromXML(bool shouldDelete)
        {
            if (shouldDelete)
            {
                delete_create();
            }
            List<Type> objs = new List<Type> { typeof(Hobbit), typeof(Wizard), typeof(Orc), typeof(Elf), typeof(Ring) };
            string finalFileName = "final.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(finalFileName);
            int i = 0;
            foreach (XmlNode elems in doc.SelectNodes("/root/*/NewDataSet"))
            {
                foreach (XmlElement node in elems.SelectNodes("Table"))
                {
                    Console.WriteLine(node.InnerXml);
                    XMLObject tmp = (XMLObject)Activator.CreateInstance(objs[i]);
                    tmp.loadXML(node);
                    Insert((SQLObject)tmp, objs[i].ToString().Split('.')[1]);
                }
                i++;
                if (i >= objs.Count) { break; }
            }
        }
    }
}