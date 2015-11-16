﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;


namespace Server.Database
{
    public class DBGeoLoc
    {
        public DBConnection DbConnection { get; private set; }
        private static DBGeoLoc instance;

        /// private constructor for singelton     
        private DBGeoLoc()
        {
            DbConnection = DBConnection.Instance;
        }

        /// singelton get instance method 
        /// returns the instance from DB 
        public static DBGeoLoc Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBGeoLoc();
                }

                return instance;
            }

        }

        /// <summary>
        /// Returns a geoLoc...
        /// </summary>
        /// <param name="defaultDeliveryStopID"></param>
        /// <returns></returns>
        public GeoLoc getGeoLoc(long defaultDeliveryStopID)
        {
            List<GeoLoc> list;
            String sql = String.Format("select * from GeoLoc where id = {0}",
                    defaultDeliveryStopID);
            list = DbConnection.SendSQL<GeoLoc>(sql, ConvertToGeoLoc);
            return list[0];
        }

        /// <summary>
        /// Return a list of all GeoPoints
        /// </summary>
        /// <returns>GeoPoints</returns>
        public List<GeoLoc> GetGeoLocs()
        {
            List<GeoLoc> list;
            String sql = String.Format("select * from GeoLoc");
            return DbConnection.SendSQL<GeoLoc>(sql, ConvertToGeoLoc);
        }

        /// <summary>
        /// Takes the ReulstSet from database and returns a list of all GeoLocs.
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        private List<GeoLoc> ConvertToGeoLoc(IDataReader dataSet)
        {
            List<GeoLoc> tableList = new List<GeoLoc>();
            while (dataSet.Read())
            {
                tableList.Add(new GeoLoc(
                    dataSet.GetInt64(0),
                    dataSet.GetDouble(1),
                    dataSet.GetDouble(2)
                ));
            }

            return tableList;
        }
    }
}