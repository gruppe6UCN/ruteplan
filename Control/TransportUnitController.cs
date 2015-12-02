﻿using System.Collections.Generic;
using Database;
using Model;

namespace Control
{
    public class TransportUnitController
    {
        public DBTransportUnit DbTransportUnit { get; private set; }
        private static TransportUnitController instance;
        public DefaultDeliveryStopController DefaultDeliveryStopCtr { get; private set; }

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private TransportUnitController() 
        {
            DbTransportUnit = DBTransportUnit.Instance;
            DefaultDeliveryStopCtr = DefaultDeliveryStopController.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static TransportUnitController Instance{
            get {
                if (instance == null)
                    instance = new TransportUnitController();
                return instance;
            }
        }

        /// <summary>
        /// Loads from the database and adds transport units to the given DeliveryStop.
        /// </summary>
        /// <param name="deliveryStop">DeliveryStop to contain TransportUnits.</param>
        /// <param name="customers">List of customers for stop.</param>
        public void AddTransportUnit(DeliveryStop deliveryStop, List<Customer> customers) {

            List<long> IDs = new List<long>();

            foreach (Customer customer in customers) 
            {
                IDs.Add(customer.ID);
            }

            deliveryStop.TransportUnits = DbTransportUnit.GetTransportUnits(IDs);
        }

        /// <summary>
        /// Loads from file imported on DefaultDeliveryStopController
        /// and adds transport units to the given DeliveryStop.
        /// </summary>
        /// <param name="deliveryStop">DeliveryStop to contain TransportUnits.</param>
        /// <param name="customers">List of customers for stop.</param>
        public void AddTransportUnitFromFile(DeliveryStop deliveryStop, List<Customer> customers)
        {




        }
    }
}
