using System;

namespace Model
{
    public class TransportUnit {

        public long ID { get; }
        public long CustomerID { get; }
        public double UnitType { get; }

        public TransportUnit(long id, long customerID, double unitType) {
            this.ID = id;
            this.UnitType = unitType;
            this.CustomerID = customerID;
        }
    }
}

