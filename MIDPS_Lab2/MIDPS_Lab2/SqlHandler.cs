using System;
using System.Data.SqlClient;

namespace SQL
{
    public class SqlHandler
    {
        string connectionString = "Data Source=DESKTOP-0FJJT76\\SQLEXPRESS;Initial Catalog=MiddleEarth;Integrated Security=True";
        public SqlHandler()
        { }

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

        public void Insert(Object temp)
        {
            switch (typeof(temp))
        }
    }
}




}