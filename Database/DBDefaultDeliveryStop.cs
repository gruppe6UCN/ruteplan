using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Database
{
    /// <summary>
    /// all database functionality for default delivery stops will be handled by this class
    /// </summary>
    public class DBDefaultDeliveryStop
    {
        public DBConnection DbConnection { get; set; }
        private static DBDefaultDeliveryStop instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DBDefaultDeliveryStop()
        {
            DbConnection = DBConnection.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>      
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultRouteID">defaultRouteID</param>
        /// <returns>list of all DefaultDeliveryStop for the given defaultRouteID</returns>
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(long defaultRouteID)
        {
            List<DefaultDeliveryStop> list;
            String sql = String.Format("select * from DefaultDeliveryStop where default_route_id = '{0}';", defaultRouteID);
            list = DbConnection.SendSQL<DefaultDeliveryStop>(sql, ConvertTotDefaultDeliveryStop);
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSet">rs takes the ResultSet from database</param>
        /// <returns>list of DefaultDeliveryStop</returns>
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

        


    

