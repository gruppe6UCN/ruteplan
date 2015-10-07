using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Server.Database
{
    class DBDeliveryStop
    {
        private DBConnection DbConnection { get; private set; }
        private static DBDeliveryStop instance; 

        /// private constructor for singelton
        private DBDeliveryStop()
        {
            DbConnection = DBConnection.Instance;
        }

        /// singelton get instance method 
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

        /// Returns the id for deliveryStop.
        public ulong store(long routeID, DeliveryStop deliveryStop)
        {
            String sql = String.Format("INSERT into DeliveryStop values(%d, %d);",
                    routeID,
                    deliveryStop.DefaultStop.ID);

            return DbConnection.SendInsertSQL(sql);
        }
    }
}

        
