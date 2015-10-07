using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Server.Database
{
    public class DBRoute
    {
        private DBConnection DbConnection { get; private set; }
        private static DBRoute instance; 

        /// private constructor for singelton     
        private DBRoute()
        {
            DbConnection = DBConnection.Instance;
        }

        /// singelton get instance method 
        /// returns the instance from DB 
        public static DBRoute Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBRoute();
                }

                return instance;
            }

        }

        /// Stores all routes in the database.
        public void storeRoute(Route route)
        {
            DateTime time = route.TimeForDeparture;
            DateTime date = route.DateForDeparture;
            String sql = String.Format("INSERT into Route values({0}, '{1}', '{2}');",
                    route.DefaultRoute.ID,
                    String.Format("{0}:{1}:{2}", time.Hour, time.Minute, time.Second),
                    String.Format("{0}-{1}-{2}", date.Year, date.Month, date.Day));
            ulong routeID = DbConnection.SendInsertSQL(sql);
            route.ID = routeID;
        }
    }
}