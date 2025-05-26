using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLibrary
{
    public class ConnectionManager
    {
        public static string ConnectionString { get { return _connectionString; }}
        private static string _connectionString = @"Data Source=mssql13.unoeuro.com;Initial Catalog=troglodyte_dk_db_troglodytedb;User ID=troglodyte_dk;Password=9Datmy4x5RrekfGng6dA;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static void TestMode()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TennisTester;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }
    }
}
