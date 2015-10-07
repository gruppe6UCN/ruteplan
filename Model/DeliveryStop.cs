using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DeliveryStop
    {
        public ulong ID { get; private set; }
        public DefaultDeliveryStop DefaultStop { get; private set; }
        public List<TransportUnit> TransportUnits { get; private set; }

        public DeliveryStop(DefaultDeliveryStop defaultStop)
        {
            this.DefaultStop = defaultStop;
            //Automatize some variables such as id later m8.
        }

        public double getSizeOfTransportUnits()
        {
            //Creates variable.
            double load = 0.0;

            //Enters a loop for each transportUnit
            foreach (TransportUnit transportUnit in TransportUnits)
            {

                //Increments load with the transportUnits size.
                load += transportUnit.UnitType;
            }

            //Returns the load.
            return load;
        }
    }

}
