﻿using System;
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

        public double getSizeOfTransportUnits()
        {
            //Creates variable.
            double load = 0.0;

            //Enters a loop for each transportUnit
            foreach (TransportUnit transportUnit in transportUnits)
            {

                //Increments load with the transportUnits size.
                load += transportUnit.UnitType;
            }

            //Returns the load.
            return load;
        }
    }

}
