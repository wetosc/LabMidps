using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

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

    public class Singleton
    {
        private static readonly Singleton instance = new Singleton();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton()
        {
        }

        private Singleton()
        {
            string connectionString = "Data Source=DESKTOP-0FJJT76\\SQLEXPRESS;Initial Catalog=MiddleEarth;Integrated Security=True";
            //string connectionString = "Data Source=TI50115;Initial Catalog=MiddleEarth;Integrated Security=True";

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
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0FJJT76\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True"))
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
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-0FJJT76\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True"))
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

    }
}