using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract()]
    public class DeliveryStop
    {
        [DataMember()]
        public long ID { get; set; }
        [DataMember()]
        public DefaultDeliveryStop DefaultStop { get; private set; }
        [DataMember()]
        public List<TransportUnit> TransportUnits { get; set; }

        public DeliveryStop(DefaultDeliveryStop defaultStop)
        {
            this.DefaultStop = defaultStop;
            this.TransportUnits = new List<TransportUnit>();
        }

        public double GetSizeOfTransportUnits()
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
