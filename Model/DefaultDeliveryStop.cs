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
        private long _GeoLogID;

        public long GeoLocID
        {
            get { return GeoLoc == null ? _GeoLogID : GeoLoc.ID; }
        }

        public GeoLoc GeoLoc { get; set; }

        public DefaultDeliveryStop(long ID, long GeoLocID)
        {
            this.ID = ID;
            this._GeoLogID = GeoLocID;
        }
    }
}
