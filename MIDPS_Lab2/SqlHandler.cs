using System;
using System.Data.SqlClient;

namespace SQL
{
    public class SqlHandler
    {
        public SqlHandler()
        {
        }

        public void Connect()
        {
            string connectionString = "user id=username;password=password;server=localhost;Trusted_Connection=yes;" +
                                           "database=MiddleEarth; connection timeout=30";

            SqlConnection myConnection = new SqlConnection(connectionString);
            
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("asda");
            }

        }
    }




}