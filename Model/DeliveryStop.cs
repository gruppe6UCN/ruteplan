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

   }

}
