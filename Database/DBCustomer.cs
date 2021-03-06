﻿using System;
using System.Collections.Generic;
using System.Data;
using Model;

namespace Database
{
    public class DBCustomer {
        public DBConnection dbConnection { set; get; }
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
        /// Initializes a new singleton of the <see cref="DBCustomer"/> class.
        /// 
        private DBCustomer() {
            dbConnection = DBConnection.Instance;
        }

        /// <summary>
        /// Gets a list of customers associated with the given default delivery stop
        /// </summary>
        /// <returns>list of all Customer for the given default delivery stop</returns>
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
                        dataSet.GetString(2),
                        dataSet.GetString(3),
                        dataSet.GetInt32(4),
                        dataSet.GetString(5),
                        (TimeSpan) dataSet.GetValue(6)
                    ));
            }
            return tableList;
        }

        /// <summary>
        /// Stores the given list of customers in the database with associated foreign key for defaultStop.
        /// </summary>
        /// <param name="customers">List of customrs for stop to save.</param>
        /// <param name="ID">ID of foreignkey DefaultStop.</param>
        public void StoreCustomers(List<Customer> customers, long ID)
        {
            foreach (Customer customer in customers)
            {
                String sql = String.Format("INSERT IGNORE into Customer values({0}, {1}, \"{2}\", \"{3}\", {4}, \"{5}\", \"{6}\");",
                    customer.ID,
                    ID,
                    customer.StreetName,
                    customer.StreetNo,
                    customer.Zipcode,
                    customer.City,
                    customer.TimeOfDelivery);

                dbConnection.SendInsertSQL(sql);
            }

        }
    }
}

