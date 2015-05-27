package model;

public class TransportUnit {
    
    private long id;
    private UnitType unitType;
    private long customerId;
    
    public TransportUnit(long id, long customerId, UnitType unitType) {
        this.id = id;
        this.unitType = unitType;
        this.customerId = customerId;
    }

    /**
     * @return the id
     */
    public long getId() {
        return id;
    }

    /**
     * @param id the id to set
     */
    public void setId(long id) {
        this.id = id;
    }

    /**
     * @return the unitType
     */
    public UnitType getUnitType() {
        return unitType;
    }

    /**
     * @param unitType the unitType to set
     */
    public void setUnitType(UnitType unitType) {
        this.unitType = unitType;
    }

    /**
     * @return the customerId
     */
    public long getCustomerId() {
        return customerId;
    }

    /**
     * @param customerId the customerId to set
     */
    public void setCustomerId(long customerId) {
        this.customerId = customerId;
    }
    

}
