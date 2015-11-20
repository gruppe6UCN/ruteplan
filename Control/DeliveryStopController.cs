using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using System.Collections.Concurrent;
using Model;

namespace Control
{
    public class DeliveryStopController
    {
        public TransportUnitController TransportUnitCtr { get; private set; }
        public DBDeliveryStop DbDeliveryStop { get; private set; }
        private static DeliveryStopController instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DeliveryStopController()
        {
            TransportUnitCtr = TransportUnitController.Instance;
            DbDeliveryStop = DBDeliveryStop.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DeliveryStopController Instance {
            get {
                if (instance == null)
                    instance = new DeliveryStopController();
                return instance;
            }
        }

        /// <summary>
        /// Stores all delivery stops for the route in the database.
        /// </summary>
        /// <param name="route">Route containing stops.</param>
        public void StoreDeliveryStops(Route route) 
        {
            var stops = route.Stops; //TODO: Use a thread-safe list instead of List<T>
            Parallel.ForEach(stops, stop =>
            {
                long deliveryStopID = DbDeliveryStop.StoreDeliveryStop(route.ID, stop);
                stop.ID = deliveryStopID;
            });
        }

        /// <summary>
        /// Creates and adds all DeliveryStops to the given route, based on the given DefaultDeliveryStop.
        /// </summary>
        /// <param name="route">Route to contain DeliveryStops</param>
        /// <param name="defaultStops">List of DefaultStops.</param>
        public void AddDeliveryStops(Route route, List<DefaultDeliveryStop> defaultStops) 
        {
            //Creates delivery stop for each default stop.
            //TODO: Use a thread safe list instead of List<T>
            defaultStops.ForEach(defaultStop =>
            {
                DeliveryStop stop = new DeliveryStop(defaultStop);
                TransportUnitCtr.AddTransportUnit(stop, stop.DefaultStop.Customers);
                route.Stops.Add(stop);
            });
        }
    }
}
