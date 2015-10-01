﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DefaultDeliveryStop
    {
        public long ID { get; private set; }
        public List<Customer> Customers { get; private set; }
        public long GeoLocID { get; private set; }

        public DefaultDeliveryStop(long ID, long GeoLocID)
        {
            this.ID = ID;
            this.GeoLocID = GeoLocID;
        }

    }
}