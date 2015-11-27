using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Database
{
    public class DBDeliveryStop
    {
        public DBConnection DbConnection { get; private set; }
        private static DBDeliveryStop instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DBDeliveryStop()
        {
            DbConnection = DBConnection.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DBDeliveryStop Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBDeliveryStop();
                }

                return instance;
            }

        }

        /// Stores given delivery stop in the database.
        /// Returns the id for deliveryStop.
        public long StoreDeliveryStop(long routeID, DeliveryStop deliveryStop)
        {
            String sql = String.Format("INSERT into DeliveryStop (route_id, default_delivery_stop_id) values({0}, {1});",
                    routeID,
                    deliveryStop.DefaultStop.ID);

            long stopID = DbConnection.SendInsertSQL(sql);
            deliveryStop.ID = stopID;
            return stopID;
        }
    }
}

        
