using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultDeliveryStop
    {
        public long ID { get; private set; }
        public List<Customer> Customers { get; set; }
        public long GeoLocID { get; private set; }
        public int? SequenceNbr { get; private set; }

        public DefaultDeliveryStop(long ID, long GeoLocID)
        {
            this.ID = ID;
            this.GeoLocID = GeoLocID;
        }

        public DefaultDeliveryStop(long ID, long GeoLocID, int? SequenceNbr)
        {
            this.ID = ID;
            this.GeoLocID = GeoLocID;
            this.SequenceNbr = SequenceNbr;
        }

    }
}
