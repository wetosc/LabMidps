using System;
using System.Data.SqlClient;

namespace SQL
{
    public class SqlHandler
    {
        public SqlConnection myConnection;
        string connectionString = "Data Source=DESKTOP-0FJJT76\\SQLEXPRESS;Initial Catalog=MiddleEarth;Integrated Security=True";
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
            string sql_insert_command = "INSERT INTO @table (@params) VALUES (@values);";
            string tableName="", param="", values="";
            if (temp is Ring) { tableName = "Ring"; param = "Material","Name"; values = String.Format("{0}','{1}", ((Ring)temp).Material, ((Ring)temp).Name); }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql_insert_command, conn);
                cmd.Parameters.AddWithValue("@table", tableName);
                cmd.Parameters.AddWithValue("@params", param);
                cmd.Parameters.AddWithValue("@values", values);
                cmd.Parameters.AddRange();
                return cmd.ExecuteNonQuery();
            }
        }
    }
}



