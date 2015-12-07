using System;
using System.IO;
using Database;

namespace Server
{
    public class DBClient
    {
        private static DBConnection dbInstance;

        /// <summary>
        /// Initialize the TestArla database for the the DBConnection.
        /// </summary>
        public static void Initialize()
        {
            Initialize("TestArla");
        }

        /// <summary>
        /// Initialize the TestArlaEmpty database for the the DBConnection.
        /// </summary>
        public static void InitializeEmpty()
        {
            Initialize("TestArlaEmpty");
        }

        /// <summary>
        /// Initialize the "database" for the the DBConnection.
        /// </summary>
        /// <param name="database">the name of the database</param>
        public static void Initialize(String database)
        {
            dbInstance = DBConnection.Instance;
            dbInstance.Host = "localhost";
            dbInstance.DB = database;
            dbInstance.User = File.ReadAllText("Config/user.txt");
            dbInstance.Pass = File.ReadAllText("Config/pass.txt");
            dbInstance.Connect();
        }

        public static void Terminate()
        {
            dbInstance.Disconnect();
        }
    }
}
