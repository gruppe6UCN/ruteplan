using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Server
{
    public class DBConnection
    {
        private static DBConnection instance;
        private MySqlConnection connection;
        private MySqlTransaction transaction;

        public String Host { get; set; }
        public String DB { get; set; }
        public String User { get; set; }
        public String Pass { get; set; }
        private const String connectionString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};";

        public bool IsConnected { get {return connection.State == System.Data.ConnectionState.Open; } }

        public static DBConnection Instance { 
            get { 
                if (instance == null)
                    instance = new DBConnection();
                return instance;
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
            transaction = connection.BeginTransaction();
        }

        public void Disconnect()
        {
            connection.Close();
        }

        public ulong SendInsertSQL(String sql) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            ulong r = 0;

            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID() FROM DefaultRoute";
            Object obj = cmd.ExecuteScalar();

            if (obj is ulong)
                r = (ulong) obj;

            transaction.Commit();
            return r; // Returns the ID for the new Row
        }

        public int SendUpdateSQL(String sql) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            int r = 0;

            cmd.CommandText = sql;
            r = cmd.ExecuteNonQuery();

            return r; // Returns a number equal to the amount of rows that changed
        }

        public delegate List<T> SqlToObject<T>(IDataReader sqlData);
        public List<T> SendSQL<T>(String sql, SqlToObject<T> sqlToObject) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;

            cmd.CommandText = sql;
            IDataReader dbData = cmd.ExecuteReader();

            List<T> r = sqlToObject(dbData);
            return r;
        }
    }
}
