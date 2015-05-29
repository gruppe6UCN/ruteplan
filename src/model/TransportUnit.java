package model;

public class TransportUnit {
    
    private long id;
    private double unitType;
    private long customerId;
    
    public TransportUnit(long id, long customerId, double unitType) {
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
    public double getUnitType() {
        return unitType;
    }
    

}
