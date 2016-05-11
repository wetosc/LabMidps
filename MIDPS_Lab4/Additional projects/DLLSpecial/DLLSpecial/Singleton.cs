using System;
using System.Data.SqlClient;
using System.Data;

namespace DLLSpecial
{
    public interface SQLObject
    {
        string select();
        string select2(int id);
        string insertString();
        string deleteString(int n);
        string updateFormat();
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

        public int Insert(SQLObject temp)
        {
            connect();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            switch (temp.GetType().ToString())
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
                        if ((int)cmd.ExecuteScalar() > 0)
                        {
                            cmd.CommandText = ((Elf)temp).insertString();
                            return cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = temp.insertString();
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

        //public int deleteOne(Type t)
        //{
        //    connect();

        //    SqlCommand cmd = new SqlCommand();
        //    SQLObject obj = (SQLObject)Activator.CreateInstance(t);
        //    cmd.CommandText = obj.deleteString(1);
        //    cmd.Connection = connection;
        //    return cmd.ExecuteNonQuery();
        //}

        //public int deleteMultiple(Type t, int n)
        //{
        //    connect();

        //    SqlCommand cmd = new SqlCommand();
        //    SQLObject obj = (SQLObject)Activator.CreateInstance(t);
        //    cmd.CommandText = obj.deleteString(n);
        //    cmd.Connection = connection;
        //    return cmd.ExecuteNonQuery();
        //}

        //public int update(Type t, string oldName, string newName)
        //{
        //    connect();
        //    SqlCommand cmd = new SqlCommand();
        //    SQLObject obj = (SQLObject)Activator.CreateInstance(t);
        //    cmd.CommandText = String.Format(obj.updateFormat(), oldName, newName);
        //    cmd.Connection = connection;
        //    return cmd.ExecuteNonQuery();
        //}

        //public void read<T>() where T : new()
        //{
        //    connect();

        //    string query = "";
        //    SQLObject obj = (SQLObject)new T();
        //    query = obj.sortString().Split(new[] { "ORDER" }, StringSplitOptions.None)[0];
        //    SqlCommand command = new SqlCommand(query, connection);
        //    using (SqlDataReader reader = command.ExecuteReader())
        //    {
        //        for (int i = 0; i < reader.FieldCount; i++)
        //        {
        //            Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
        //        }
        //        Console.WriteLine(); Console.WriteLine();

        //        while (reader.Read())
        //        {
        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
        //            }
        //            Console.WriteLine();
        //        }
        //        reader.Close();
        //    }
        //    connection.Close();
        //}

        //public void readSpecial(int cas, int id)
        //{
        //    connect();

        //    string query = "";
        //    switch (cas)
        //    {
        //        case 0:
        //            query = String.Format("SELECT * FROM Ring WHERE id IN (SELECT Ring_ID FROM Master2Ring WHERE Master_ID = '{0}');", id);
        //            break;
        //        case 1:
        //            query = String.Format("SELECT * FROM Wizard WHERE id IN (SELECT Master_ID FROM Master2Ring WHERE Ring_ID = '{0}');", id);
        //            break;
        //        case 2:
        //            query = String.Format("SELECT * FROM Wizard WHERE Hobbit_Friend = '{0}'; SELECT * FROM Elf WHERE Hobbit_Friend = '{0}'", id);
        //            break;
        //    }


        //    if (cas == 2)
        //    {
        //        SqlCommand command = new SqlCommand(query.Split(';')[0], connection);
        //        Console.WriteLine();
        //        Console.WriteLine("----------   Wizard Friends ----------");
        //        Console.WriteLine();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
        //            }
        //            Console.WriteLine(); Console.WriteLine();

        //            while (reader.Read())
        //            {
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
        //                }
        //                Console.WriteLine();
        //            }
        //            reader.Close();
        //        }
        //        command = new SqlCommand(query.Split(';')[1], connection);
        //        Console.WriteLine();
        //        Console.WriteLine("----------   Elf Friends ----------");
        //        Console.WriteLine();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
        //            }
        //            Console.WriteLine(); Console.WriteLine();

        //            while (reader.Read())
        //            {
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
        //                }
        //                Console.WriteLine();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    else
        //    {
        //        SqlCommand command = new SqlCommand(query, connection);

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            for (int i = 0; i < reader.FieldCount; i++)
        //            {
        //                Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
        //            }
        //            Console.WriteLine(); Console.WriteLine();

        //            while (reader.Read())
        //            {
        //                for (int i = 0; i < reader.FieldCount; i++)
        //                {
        //                    Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
        //                }
        //                Console.WriteLine();
        //            }
        //            reader.Close();
        //        }
        //    }
        //    connection.Close();

        //}

    }
}