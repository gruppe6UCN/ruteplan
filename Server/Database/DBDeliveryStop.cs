using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Server.Database
{
    public class DBDeliveryStop
    {
        class TmpDefaultDeliveryStop
        {
            public long ID { get; private set; }
            public long IDDefaultStop { get; private set; }
            public TmpDefaultDeliveryStop(long ID, long IDDefaultStop)
            {
                this.ID = ID;
                this.IDDefaultStop = IDDefaultStop;               
            }
        }

        public DBConnection DbConnection { get; private set; }
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

        //Returns a list of all delivery stops.
        //TODO - Optimize with lambda...
        public List<DeliveryStop> GetDeliveryStops(List<DefaultDeliveryStop> defaultStops)
        {
            List<TmpDefaultDeliveryStop> tmpList;
            string sql = string.Format("select * from DeliveryStop;");
            tmpList = DbConnection.SendSQL<TmpDefaultDeliveryStop>(sql, ConvertToDeliveryStop);

            List<DeliveryStop> list = new List<DeliveryStop>();

            defaultStops.ForEach(s => {
                foreach (TmpDefaultDeliveryStop tmpStop in tmpList)
                {
                    if (tmpStop.IDDefaultStop == s.ID)
                    {
                        DeliveryStop stop = new DeliveryStop(s);
                        stop.ID = tmpStop.ID;
                        list.Add(stop);
                    }
                }
            });

            return list;
        }

        // Converts database string to a model class.
        private List<TmpDefaultDeliveryStop> ConvertToDeliveryStop(IDataReader dataSet)
        {
            List<TmpDefaultDeliveryStop> tableList = new List<TmpDefaultDeliveryStop>();
            while (dataSet.Read())
            {
                tableList.Add(new TmpDefaultDeliveryStop(

                    dataSet.GetInt64(0),
                    dataSet.GetInt64(1)
                    ));
            }

            return tableList;
        }      
    }
}

        
