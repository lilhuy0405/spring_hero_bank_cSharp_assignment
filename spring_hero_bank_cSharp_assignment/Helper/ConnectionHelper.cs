using System;
using MySql.Data.MySqlClient;

namespace spring_hero_bank_cSharp_assignment.Controller
{
    public class ConnectionHelper
    {
        private const string DatabaseServer = "127.0.0.1";
        private const string DatabaseName = "asm-c#-spring-hero-bank";
        private const string DatabaseUid = "root";
        private const string DatabasePassword = "";
        private static MySqlConnection _connection;

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                Console.WriteLine("Create new connection...");
                _connection =
                    new MySqlConnection(
                        $"SERVER={DatabaseServer};DATABASE={DatabaseName};UID={DatabaseUid};PASSWORD={DatabasePassword}");
                Console.WriteLine("...success!");
            }

            return _connection;
        }
    }
}