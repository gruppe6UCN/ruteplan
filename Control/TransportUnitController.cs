using System.Collections.Generic;
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
            
            foreach (var tmpStop in DefaultDeliveryStopCtr.TmpDefaultStops)
            {
                if (tmpStop.ID == deliveryStop.DefaultStop.ID)
                {
                    deliveryStop.TransportUnits = CreateTransportUnits(tmpStop.RCE, tmpStop.CustomerID);
                }
            }
        }

        /// <summary>
        /// Calculactes and creates a list of transport units from the given total.
        /// Each transport unit have 1 size, and last transport unit gets the remaining.
        /// </summary>
        /// <param name="total">Total load for stop.</param>
        /// <returns>List of transport units.</returns>
        public List<TransportUnit> CreateTransportUnits(double total, long cID)
        {
            List<TransportUnit> units = new List<TransportUnit>();

            for (long i = 0; i <= total; i++)
            {
                //Checks if over total.
                if (total - i < 1)
                {
                    double delta = total - i;
                    TransportUnit tu = new TransportUnit(i, cID, delta);
                    DbTransportUnit.StoreTransportUnit(tu, cID);
                    units.Add(tu);
                }
                else
                {
                    TransportUnit tu = new TransportUnit(i, cID, 1);
                    DbTransportUnit.StoreTransportUnit(tu, cID);
                    units.Add(tu);
                }
            }

            return units;
        }

    }
}
