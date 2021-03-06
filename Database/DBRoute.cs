﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Database
{
    public class DBRoute
    {
        class TmpRoute
        {
            public long ID { get; set; }
            public long DefaultRouteID { get; private set; }
            public List<DeliveryStop> Stops { get; private set; }
            public DateTime TimeForDeparture { get; private set; }
            public DateTime DateForDeparture { get; private set; }

            public TmpRoute(long id, long DefaultRouteID, DateTime time, DateTime Date)
            {
                this.ID = id;
                this.DefaultRouteID = DefaultRouteID;
                this.DateForDeparture = Date;
                this.TimeForDeparture = time;
            }
        }
        
        public DBConnection DbConnection { get; private set; }
        private static DBRoute instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>   
        private DBRoute()
        {
            DbConnection = DBConnection.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
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

        //Returns a list of all routes.
        //TODO - Optimize with lambda...
        public List<Route> GetRoutes(List<DefaultRoute> defaultRoutes)
        {
            List<TmpRoute> tmpList;
            string sql = string.Format("select * from Route;");
            tmpList = DbConnection.SendSQL<TmpRoute>(sql, ConvertToRoute);

            List<Route> list = new List<Route>();

            defaultRoutes.ForEach(s =>
            {
                foreach (TmpRoute tmpRoute in tmpList)
                {
                    if (tmpRoute.DefaultRouteID == s.ID)
                    {
                        Route route = new Route(s, tmpRoute.DateForDeparture);
                        route.ID = tmpRoute.ID;
                        list.Add(route);
                    }
                }
            });

            return list;
        }

        // Converts database string to a model class.
        private List<TmpRoute> ConvertToRoute(IDataReader dataSet)
        {
            List<TmpRoute> tableList = new List<TmpRoute>();
            while (dataSet.Read())
            {
                tableList.Add(new TmpRoute(
                    dataSet.GetInt64(0),
                    dataSet.GetInt64(1),
                    dataSet.GetDateTime(2),
                    dataSet.GetDateTime(3)
                    ));
            }

            return tableList;
        }  


        /// Stores all routes in the database.
        public long storeRoute(Route route)
        {
            TimeSpan time = route.TimeForDeparture;
            DateTime date = route.DateForDeparture;
            string sql = string.Format("INSERT into Route (default_route_id, time_for_expected_departure, date_for_departure) values({0}, '{1}', '{2}');",
                route.DefaultRoute.ID,
                String.Format("{0}:{1}:{2}", time.Hours, time.Minutes, time.Seconds),
                String.Format("{0}-{1}-{2}", date.Year, date.Month, date.Day));

            long routeID = DbConnection.SendInsertSQL(sql);
            route.ID = routeID;
            return routeID;
        }
    }
}