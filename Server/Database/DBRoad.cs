using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Server.Database
{
    public class DBRoad
    {
        public DBConnection DbConnection { get; private set; }
        private static DBRoad instance;

        /// private constructor for singelton     
        private DBRoad()
        {
            DbConnection = DBConnection.Instance;
        }

        /// singelton get instance method 
        /// returns the instance from DB 
        public static DBRoad Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBRoad();
                }

                return instance;
            }

        }

        /// <summary>
        /// Returns a list of all Roads.
        /// </summary>
        /// <param name="defaultDeliveryStopID"></param>
        /// <returns></returns>
        public List<Road> getRoads()
        {
            List<Road> list;
            String sql = "select * from Road";
            list = DbConnection.SendSQL<Road>(sql, ConvertToRoad);
            return list;
        }


        /// <summary>
        /// Takes the ReulstSet from database and returns a list of all Roads.
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public List<Road> ConvertToRoad(IDataReader dataSet) 
        {
            List<Road> tableList = new List<Road>();
            while (dataSet.Read()) {
                tableList.Add(new Road(
                    dataSet.GetInt64(0),
                    dataSet.GetInt64(1),
                    dataSet.GetDouble(2),
                    (TimeSpan)dataSet.GetValue(3)
                ));
            }
            
            return tableList;
        }
    }
}