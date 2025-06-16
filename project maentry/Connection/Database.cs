using System;
using Npgsql;

namespace maentry
{
    public class Database
    {
        private static string connString = "Host=localhost;Port=5432;Username=postgres;Password=yourDBpassword;Database=yourDBname";

        public static NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(connString);
            return conn;
        }
    }
}
