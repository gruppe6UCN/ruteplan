using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Server.Database
{
    public class DBTransportUnit
    {
        private DBConnection DbConnection { get; set; }
        private static DBTransportUnit instance;

        /// private constructor for singelton     
        private DBTransportUnit()
        {
            DbConnection = DBConnection.Instance;
        }

        /// singelton get instance method 
        /// returns the instance from DB 
        public static DBTransportUnit Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBTransportUnit();
                }

                return instance;
            }

        }

        /// <summary>
        /// Returns a list of all TransportUnits.
        /// </summary>
        /// <param name="defaultDeliveryStopID"></param>
        /// <returns></returns>
        public List<TransportUnit> getTransportUnits(List<long> IDs)
        {
            List<TransportUnit> list = new List<TransportUnit>();

            foreach (long id in IDs) 
            {
                List<TransportUnit> tmp_list;
                String sql = String.Format("select * from TransportUnit where customer_id = '{0}';", id);
                tmp_list = DbConnection.SendSQL<TransportUnit>(sql, ConvertToTransportUnit);
                foreach (TransportUnit t in tmp_list) {
                    list.Add(t);
                }
            }
            return list;
        }


        /// <summary>
        /// Takes the ReulstSet from database and returns a list of all TransportUnits.
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public List<TransportUnit> ConvertToTransportUnit(IDataReader dataSet) 
        {
            List<TransportUnit> tableList = new List<TransportUnit>();
            while (dataSet.Read()) {
                tableList.Add(new TransportUnit(
                    dataSet.GetInt64(0),
                    dataSet.GetInt64(1),
                    dataSet.GetDouble(2)
                ));
            }
            
            return tableList;
        }
    }
}