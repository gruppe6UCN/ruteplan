using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace Database
{
    public class DBConnection
    {
        public delegate List<T> SqlToObject<T>(IDataReader sqlData);

        private static DBConnection instance;
        private MySqlConnection connection;
        private MySqlTransaction transaction;

        public String Host { get; set; }
        public String DB { get; set; }
        public String User { get; set; }
        public String Pass { get; set; }
        private const String connectionString = "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};Convert Zero Datetime=True;";

        public bool IsConnected { get {return connection.State == ConnectionState.Open; } }

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

        /// <summary>
        /// Connect to The sql database
        /// </summary>
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

        /// <summary>
        /// Disconnect from the sql database
        /// </summary>
        public void Disconnect()
        {
            connection.Close();
        }

        /// <summary>
        /// Add data to sql database
        /// </summary>
        /// <returns>The the id for the added data</returns>
        /// <param name="sql">Sql command</param>
        public long SendInsertSQL(String sql) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            long r = 0;

            lock (connection)
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT LAST_INSERT_ID() FROM DefaultRoute";
                Object obj = cmd.ExecuteScalar();

                if (obj is ulong)
                    r = Convert.ToInt64(obj);
            }

            return r; // Returns the ID for the new Row
        }

        /// <summary>
        /// Updates a row(s) in the sql database
        /// </summary>
        /// <returns>Return the number</returns>
        /// <param name="sql">Sql command</param>
        public int SendUpdateSQL(String sql) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            int r = 0;

            lock (connection)
            {
                cmd.CommandText = sql;
                r = cmd.ExecuteNonQuery();
            }

            return r; // Returns a number equal to the amount of rows that changed
        }

        /// <summary>
        /// This method converts the database set to a list of type specified objects
        /// </summary>
        /// <returns>List of object</returns>
        /// <param name="sql">Sql command</param>
        /// <param name="sqlToObject">The method for converting database set</param>
        /// <typeparam name="T">The object type insite the returned list</typeparam>
        public List<T> SendSQL<T>(String sql, SqlToObject<T> sqlToObject)
        {
            MySqlCommand cmd = new MySqlCommand();
            List<T> r = null;

            cmd.Connection = connection;
            cmd.Transaction = transaction;

            cmd.CommandText = sql;
            lock (connection)
            {
                IDataReader dbData = cmd.ExecuteReader();

                r = sqlToObject(dbData);
                dbData.Close();
            }

            return r;
        }
    }
}
