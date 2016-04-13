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
            cmd.CommandText = temp.insertString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
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
                    query = String.Format("SELECT * FROM Ring WHERE id = (SELECT Ring_ID FROM Master2Ring WHERE Master_ID = '{0}')",id);
                    break;
                case 1:
                    query = String.Format("SELECT * FROM Wizard WHERE id IN (SELECT Master_ID FROM Master2Ring WHERE Ring_ID = '{0}')", id);
                    break;
                case 2:
                    query = String.Format("SELECT * FROM Ring WHERE id IN (SELECT Ring_ID FROM Master2Ring WHERE Ring_ID = '{0}')", id);
                    break;
            }
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

    }
}



