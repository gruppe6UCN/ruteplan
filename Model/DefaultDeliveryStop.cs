using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultDeliveryStop
    {
        private long ID { get; private set; }
        private List<Customer> Customers { get; private set; }
        private long GeoLocID { get; private set; }

        public DefaultDeliveryStop(long ID, long GeoLocID)
        {
            this.ID = ID;
            this.GeoLocID = GeoLocID;
        }

    }
}
