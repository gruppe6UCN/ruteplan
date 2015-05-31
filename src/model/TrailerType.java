package model;

public enum TrailerType {
    // Measured in 'rulle-paller', if I remember correctly a EU-pal is 1.4 'rulle-palle'
    STOR (51.0);
    
    private final double capacity;
    
    TrailerType(double capacity) {
        this.capacity = capacity;
    }

    /**
     * @return the capacity
     */
    public double getCapacity() {
        return capacity;
    }    

}
