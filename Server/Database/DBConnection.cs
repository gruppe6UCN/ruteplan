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

        public bool IsConnected { get {return connection.State == System.Data.ConnectionState.Open; } }

        public static DBConnection Instance { 
            get { 
                if (instance == null)
                    instance = new DBConnection(); return instance;
            }
        }

        private DBConnection()
        {
            connection = new MySqlConnection();
        }

        public void Connect()
        {
            if (Host == null)
                throw new NullReferenceException("You need to initialize the accessor 'Host'");
            else if (DB == null)
                throw new NullReferenceException("You need to initialize the accessor 'DB'");
            else if (User == null)
                throw new NullReferenceException("You need to initialize the accessor 'User'");
            else if (Pass == null)
                throw new NullReferenceException("You need to initialize the accessor 'Pass'");


            connection.ConnectionString = String.Format(connectionString, Host, DB, User, Pass);
            connection.Open();
        }

        public void Disconnect()
        {
            connection.Close();
        }
    }
}
