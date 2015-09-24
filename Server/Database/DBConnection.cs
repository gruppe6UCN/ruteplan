using System;
using MySql.Data.MySqlClient;
using System.IO;

namespace Server
{
    public class DBConnection
    {
        private static DBConnection instance;
        private MySqlConnection connection;

        public String Host { get; set; }
        public String DB { get; set; }
        public String User { get; set; }
        public String Pass { get; set; }
        private const String connectionString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};";

        private DBConnection()
        {
            if (Host == null)
                throw new NullReferenceException("You need to set the Host");
            connection = new MySqlConnection(String.Format(connectionString, Host, DB, User, Pass));
        }

        public void Connect()
        {
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Clone();
        }
    }
}

