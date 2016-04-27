using System;
using System.Data.SqlClient;

namespace SQL
{
    public interface SQLObject
    {
        string insertString();
        string deleteString(int n);
        string updateFormat();
        string sortString();
    }


    public class SqlHandler
    {
        public SqlConnection myConnection;
        //string connectionString = "Data Source=DESKTOP-0FJJT76\\SQLEXPRESS;Initial Catalog=MiddleEarth;Integrated Security=True";
        string connectionString = "Data Source=TI50115;Initial Catalog=MiddleEarth;Integrated Security=True";
        public SqlHandler() { }

        public void Connect()
        {
            SqlConnection myConnection = new SqlConnection(connectionString);
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection Error >>>" + e.ToString());
            }
            finally
            {
                Console.WriteLine("Connection state: " + myConnection.State.ToString() + "\nPress enter.");
                Console.ReadLine();
            }
        }

        public int Insert(SQLObject temp)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
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
                                cmd.CommandText = ((Elf)temp).insertWithFriendString();
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
        }

        public int deleteOne(Type t)
        {
            SqlCommand cmd = new SqlCommand();
            SQLObject obj = (SQLObject)Activator.CreateInstance(t);
            cmd.CommandText = obj.deleteString(1);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }

        public int deleteMultiple(Type t, int n)
        {
            SqlCommand cmd = new SqlCommand();
            SQLObject obj = (SQLObject)Activator.CreateInstance(t);
            cmd.CommandText = obj.deleteString(n);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }

        public int update(Type t, string oldName, string newName)
        {
            SqlCommand cmd = new SqlCommand();

            SQLObject obj = (SQLObject)Activator.CreateInstance(t);
            cmd.CommandText = String.Format(obj.updateFormat(), oldName, newName);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
            return 0;
        }

        public void sort<T>() where T : new()
        {
            string query = "";
            SQLObject obj = (SQLObject)new T();
            query = obj.sortString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
                    }
                    Console.WriteLine(); Console.WriteLine();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
                        }
                        Console.WriteLine();
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        public void read<T>() where T : new()
        {
            string query = "";
            SQLObject obj = (SQLObject)new T();
            query = obj.sortString().Split(new[] { "ORDER" }, StringSplitOptions.None)[0];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
                    }
                    Console.WriteLine(); Console.WriteLine();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
                        }
                        Console.WriteLine();
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        public void readSpecial(int cas, int id)
        {
            string query = "";
            switch (cas)
            {
                case 0:
                    query = String.Format("SELECT * FROM Ring WHERE id IN (SELECT Ring_ID FROM Master2Ring WHERE Master_ID = '{0}');", id);
                    break;
                case 1:
                    query = String.Format("SELECT * FROM Wizard WHERE id IN (SELECT Master_ID FROM Master2Ring WHERE Ring_ID = '{0}');", id);
                    break;
                case 2:
                    query = String.Format("SELECT * FROM Wizard WHERE Hobbit_Friend = '{0}'; SELECT * FROM Elf WHERE Hobbit_Friend = '{0}'", id);
                    break;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (cas == 2)
                {
                    SqlCommand command = new SqlCommand(query.Split(';')[0], connection);
                    connection.Open();
                    Console.WriteLine();
                    Console.WriteLine("----------   Wizard Friends ----------");
                    Console.WriteLine();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
                        }
                        Console.WriteLine(); Console.WriteLine();

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
                            }
                            Console.WriteLine();
                        }
                        reader.Close();
                    }
                    command = new SqlCommand(query.Split(';')[1], connection);
                    Console.WriteLine();
                    Console.WriteLine("----------   Elf Friends ----------");
                    Console.WriteLine();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
                        }
                        Console.WriteLine(); Console.WriteLine();

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
                            }
                            Console.WriteLine();
                        }
                        reader.Close();
                    }
                }
                else
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(String.Format("  {0,-10} ", reader.GetName(i)));
                        }
                        Console.WriteLine(); Console.WriteLine();

                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(String.Format("  {0,-10} ", reader.GetValue(i).ToString()));
                            }
                            Console.WriteLine();
                        }
                        reader.Close();
                    }
                }
                connection.Close();
            }
        }

    }
}



