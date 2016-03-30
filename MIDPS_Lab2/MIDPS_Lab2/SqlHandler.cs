using System;
using System.Data.SqlClient;

namespace SQL
{
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

        public int Insert(Object temp)
        {
            SqlCommand cmd = new SqlCommand();

            if (temp is Ring)
            {
                cmd.CommandText = "INSERT INTO Ring (Material,Name) VALUES (@value1,@value2);";
                cmd.Parameters.AddWithValue("@value1",((Ring)temp).Material);
                cmd.Parameters.AddWithValue("@value2", ((Ring)temp).Name);
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                return cmd.ExecuteNonQuery();
            }
        }
    }
}



