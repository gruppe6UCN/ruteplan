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
        /// Gets a list of all default delivery stops from the database, for the given default route.
        /// </summary>
        /// <param name="defaultRouteID">Id of route to get stops from.</param>
        /// <returns>List of default stops.</returns>
        public List<DefaultDeliveryStop> GetDefaultDeliveryStops(long defaultRouteID)
        {
            List<DefaultDeliveryStop> list;
            String sql = String.Format("select * from DefaultDeliveryStop where default_route_id = '{0}';", defaultRouteID);
            list = DbConnection.SendSQL<DefaultDeliveryStop>(sql, ConvertTotDefaultDeliveryStop);
            return list;
        }

        /// <summary>
        /// Converts data from the database to model classes.
        /// </summary>
        /// <param name="dataSet">Dataset to read from.</param>
        /// <returns>List of default Stops.</returns>
        private List<DefaultDeliveryStop> ConvertTotDefaultDeliveryStop(IDataReader dataSet)
        {
           List<DefaultDeliveryStop> tableList = new List<DefaultDeliveryStop>();
                while (dataSet.Read())
                {
                    tableList.Add(new DefaultDeliveryStop(
                                    dataSet.GetInt64(0),
                                    dataSet.GetInt64(2)
                            ));
                }

            return tableList;
        }


        /// <summary>
        /// Stores all DefaultDeliveryStops to the database... Funtime!
        /// WARNING, USES HAX! DANGER DANGER!?! Only to be used in import from file.
        /// </summary>
        /// <param name="stops">List of all stops to be stored.</param>
        /// <param name="defaultRouteID">ID for foreign key to use as value for default route.</param>
        public void StoreDefaultDeliveryStopHAX(List<DefaultDeliveryStop> stops, long defaultRouteID)
        {
            lock (DbConnection)
            {
                //HAX!
                DbConnection.SendUpdateSQL("SET FOREIGN_KEY_CHECKS = 0; ALTER TABLE DefaultDeliveryStop CHANGE `id` `id` BIGINT NOT NULL;");

                foreach (DefaultDeliveryStop stop in stops)
                {
                    //Normal Stuff
                    String sql = String.Format("INSERT IGNORE into DefaultDeliveryStop (id, default_route_id, geo_loc_id) values({0}, {1}, {2});",
                        stop.ID,
                        defaultRouteID,
                        stop.GeoLoc.ID);
                    DbConnection.SendInsertSQL(sql);
                }

                //Unhax...
                DbConnection.SendUpdateSQL("ALTER TABLE DefaultDeliveryStop CHANGE `id` `id` BIGINT NOT NULL AUTO_INCREMENT; SET FOREIGN_KEY_CHECKS = 1;");
            }
        }

    }
}

        


    

