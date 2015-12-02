using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract()]
    public class Route
    {
        [DataMember()]
        public long ID { get; set; }
        [DataMember()]
        public DefaultRoute DefaultRoute { get; private set; }
        [DataMember()]
        public List<DeliveryStop> Stops { get; private set; }
        [DataMember()]
        public TimeSpan TimeForDeparture { get; set; }
        [DataMember()]
        public DateTime DateForDeparture { get; private set; }

        public Route(DefaultRoute DefaultRoute, DateTime Date)
        {
            this.DefaultRoute = DefaultRoute;
            this.DateForDeparture = Date;
            this.Stops = new List<DeliveryStop>();
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
                Load += Stop.GetSizeOfTransportUnits();
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
       * Checks to see if route is under loaded.
       * @return True if it is and false vice versa
       */
        public Boolean IsUnderloaded()
        {
            return this.GetLoadForTrailer() < this.GetCapacity() * 0.8;
        }
    }
}
