﻿using System;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract()]
    public class TransportUnit
    {

        [DataMember()]
        public long ID { get; private set; }
        [DataMember()]
        public long CustomerID { get; private set; }
        [DataMember()]
        public double UnitType { get; private set; }
        
        public TransportUnit(long id, long customerID, double unitType) {
            this.ID = id;
            this.UnitType = unitType;
            this.CustomerID = customerID;
        }
    }
}

