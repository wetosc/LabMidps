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
            string connectionString = "Data Source=localhost;" +
                                      "Initial Catalog=MiddleEarth;" +
                                      "User id=wasea;" +
                                      "Password=123456;";

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