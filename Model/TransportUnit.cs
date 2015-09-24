using System;

namespace Model
{
    public class TransportUnit {

        public long ID { get; private set; }
        public long CustomerID { get; private set; }
        public double UnitType { get; private set; }

        public TransportUnit(long id, long customerID, double unitType) {
            this.ID = id;
            this.UnitType = unitType;
            this.CustomerID = customerID;
        }
    }
}

