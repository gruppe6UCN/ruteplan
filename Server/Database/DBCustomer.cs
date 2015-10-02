using System;
using System.Collections.Generic;
using Model;
using System.Data;

namespace Server
{
    public class DBCustomer {
        private DBConnection dbConnection;
        private static DBCustomer instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static DBCustomer Instance { 
            get { 
                if (instance == null)
                    instance = new DBCustomer();
                return instance;
            }
        }

        /// <summary>
        /// Initializes a new singleton of the <see cref="Server.DBCustomer"/> class.
        /// 
        private DBCustomer() {
            dbConnection = DBConnection.Instance;
        }

        /// <summary>
        /// Gets list of customers for database
        /// </summary>
        /// <returns>list of all Customer for the given id</returns>
        /// <param name="id">id from a DefaultDeliveryStop</param>
        public List<Customer> GetCustomers(long id) {
            List<Customer> list;
            String sql = String.Format("select * from Customer where default_delivery_stop_id = '{0}';", id);
            list = dbConnection.SendSQL<Customer>(sql, ConvertToCustomer);
            return list;
        }

        /// <summary>
        /// Formats the customer.
        /// </summary>
        /// <returns>The list of customer</returns>
        /// <param name="dataSet">The data set from database</param>
        private List<Customer> ConvertToCustomer(IDataReader dataSet) {
            List<Customer> tableList = new List<Customer>();
            while (dataSet.Read()) {
                tableList.Add(
                    new Customer(
                        dataSet.GetInt64(0),
                        dataSet.GetString(1),
                        dataSet.GetString(2),
                        dataSet.GetInt32(3),
                        dataSet.GetString(4),
                        dataSet.GetDateTime(5).ToLocalTime()
                    ));
            }
            return tableList;
        }
    }
}

