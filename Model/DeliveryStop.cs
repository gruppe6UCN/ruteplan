using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DeliveryStop
    {
        public long id { get; private set; }
        public DefaultDeliveryStop defaultStop { get; private set; }
        public List<TransportUnit> transportUnits { get; private set; }

        public DeliveryStop(DefaultDeliveryStop defaultStop)
        {
            this.defaultStop = defaultStop;
            //Automatize some variables such as id later m8.
        }

        /**
         *
         * @return the sum of transport units for this stop
         */
        public double GetSizeOfTransportUnits() {
        //Creates variable.
        double Load = 0.0;

        //Enters a loop for each transportUnit
        for (TransportUnit TransportUnit:TransportUnits) {

            //Increments load with the transportUnits size.
            Load += TransportUnit.GetUnitType();
        }

        //Returns the load.
        return Load;
    }
  }

}
