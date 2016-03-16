using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQL;
namespace MIDPS_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlHandler sqlHandler = new SqlHandler();
            sqlHandler.Connect();
        }
    }
}
