using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Server.Database

{
    /// all database functionality for default delivery stops will be handled by this class
    /// 
    /// </summary>
 

    public class DBDefaultDeliveryStop
    {
        public DBConnection DbConnection { get; set; }
        private static DBDefaultDeliveryStop instance; 

        /// <summary >
        /// private constructor for singelton
        /// </summary>
       
        private DBDefaultDeliveryStop()
        {
            DbConnection = DBConnection.Instance;
        }

        /// <sunmmary>
        /// singelton get instance method 
        /// returns the instance from DB 
      
        public static DBDefaultDeliveryStop Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBDefaultDeliveryStop();
                }

                return instance;
            }

        }

        /**
     * @param defaultRouteID
     * @return list of all DefaultDeliveryStop for the given defaultRouteID
     */
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(long defaultRouteID)
        {
           List<DefaultDeliveryStop> list;
            String sql = String.Format("select * from DefaultDeliveryStop where default_route_id = '{0}';", defaultRouteID);
            list = DbConnection.SendSQL<DefaultDeliveryStop>(sql, ConvertTotDefaultDeliveryStop);
            return list;
        }


        /**
         * @param rs takes the ResultSet from database
         * @return list of DefaultDeliveryStop
         */
        private List<DefaultDeliveryStop> ConvertTotDefaultDeliveryStop(IDataReader dataSet)
        {
           List<DefaultDeliveryStop> tableList = new List<DefaultDeliveryStop>();
                while (dataSet.Read())
                {
                    tableList.Add(new DefaultDeliveryStop(
                                    dataSet.GetInt64(0),
                                    dataSet.GetInt64(1)
                            ));
                }

            return tableList;
        }

    }
}

        


    

