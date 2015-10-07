using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Route
    {

        public ulong ID { get; set; }
        public DefaultRoute DefaultRoute { get; private set; }
        public List<DeliveryStop> Stops { get; private set; }
        public DateTime TimeForDeparture { get; private set; }
        public DateTime DateForDeparture { get; private set; }

        public Route(DefaultRoute DefaultRoute, DateTime Date)
        {
            this.DefaultRoute = DefaultRoute;
            this.DateForDeparture = Date;
            this.Stops = new List<DeliveryStop>();

            //Automatize dem other variables here later...
        }

        /**
         * @return the load of transport units in the trailer
         */
        public double GetLoadForTrailer()
        {
            double Load = 0.0;

            //Enters a loop for each delivery stop.
            foreach (DeliveryStop Stop in Stops)
            {
                Load += Stop.getSizeOfTransportUnits();
            }

            return Load;
        }

        /**
         * @return the capacity found under default route
         */
        public double GetCapacity()
        {
            return DefaultRoute.TrailerType;
        }

        /**
         * @param stop deliveryStop to add toList.
         */
        public void AddDeliveryStop(DeliveryStop Stop)
        {
            Stops.Add(Stop);
        }

        /**
       * Checks to see if route is under loaded.
       * @return True if it is and false vice versa
       */
        public Boolean IsUnderloaded()
        {
            return this.GetLoadForTrailer() < this.GetCapacity() * 0.8;
        }
    }
}
